using Application.Interfaces;
using Application.UseCases.Payments.Constants.Enums;
using Application.UseCases.Payments.Constants.Messages;
using Application.UseCases.Refunds.Constants.Enums;
using Application.UseCases.Refunds.Constants.Messages;
using Application.UseCases.Refunds.Specifications;
using Domain.Entities;
using ExternalServiceCommunication.Models.Refunds;
using ExternalServiceCommunication.Services.Interfaces;
using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace Application.UseCases.Refunds.Jobs
{
    public class RefundJobs : IRefundJobs
    {
        private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;
        private readonly IRepositoryAsync<Refund> _refundRepository;
        private readonly IRefundsService _refundService;

        private HttpResponseMessage ResponseMessage { get; set; } = new HttpResponseMessage() { StatusCode = HttpStatusCode.NotAcceptable };
        private Refund? Refund { get; set; }
        private bool UpdatedInDB { get; set; } = false;
        private string ResultMessage { get; set; } = string.Empty;

        public RefundJobs(IRepositoryAsync<Refund> refundRepository, IRefundsService refundService)
        {
            _refundRepository = refundRepository;
            _refundService = refundService;
            _retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotAcceptable)
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(3));
        }



        public async Task<string> UpdateStatus(int id, string externalId)
        {
            await SendRequest(id, externalId);
            if (!ResponseMessage.IsSuccessStatusCode)
            {
                await Cancel();
            }
            return ResultMessage;

        }


        private async Task<HttpResponseMessage> SendRequest(int id, string externalId)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                Refund = await _refundRepository.FirstOrDefaultAsync(new GetRefundByIdSpecification(id));
                if (Refund is not null && Refund.RefundStatusId == (int)EnumPaymentStatus.Requested)
                {
                    var externalResponse = await _refundService.Status(externalId);
                    if (externalResponse.Success && externalResponse.Data.Status != "REQUEST")
                    {
                        await Update(externalResponse.Data);
                        ResultMessage = PaymentsMessages.UpdatedInJob();
                        ResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.Accepted };
                    }

                }
                else
                {
                    UpdatedInDB = true;
                    ResultMessage = PaymentsMessages.UpdatedInDB();
                    ResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.Accepted };

                }
                return ResponseMessage;
            });
            return ResponseMessage;
        }


        private async Task Update(RefundResponse externalResponse)
        {
            if (Refund is not null)
            {
                Refund.RefundStatusId = (int)EnumRefundStatus.Requested;
                switch (externalResponse.Status)
                {
                    case "REQUEST":
                        Refund.RefundStatusId = (int)EnumRefundStatus.Requested;
                        break;
                    case "CANCELLED":
                        Refund.RefundStatusId = (int)EnumRefundStatus.Cancelled;
                        break;
                    case "CONFIRMED":
                        Refund.RefundStatusId = (int)EnumRefundStatus.Confirmed;
                        break;
                    case "UNDONE":
                        Refund.RefundStatusId = (int)EnumRefundStatus.Undone;
                        break;
                    default:
                        break;
                }
                await _refundRepository.UpdateAsync(Refund);
            }
        }
        private async Task Cancel()
        {
            if (Refund is not null)
            {
                var externalResponse = await _refundService.Cancel(Refund.ExternalId);
                if (externalResponse.Success)
                {
                    Refund.RefundStatusId = (int)EnumRefundStatus.Undone;
                    await _refundRepository.UpdateAsync(Refund);
                    ResultMessage = RefundsMessages.Canceled();

                }
                else
                {
                    ResultMessage = RefundsMessages.CanceledError();
                }

            }
        }
    }
}
