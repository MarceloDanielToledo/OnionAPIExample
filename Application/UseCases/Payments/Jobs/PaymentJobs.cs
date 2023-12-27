using Application.Constants.Messages;
using Application.Interfaces;
using Application.UseCases.Payments.Constants.Enums;
using Application.UseCases.Payments.Constants.Messages;
using Application.UseCases.Payments.Specifications;
using Domain.Entities;
using ExternalServiceCommunication.Models.Name;
using Polly;
using Polly.Extensions.Http;
using System.ComponentModel;
using System.Net;

namespace Application.UseCases.Payments.Jobs
{
    public class PaymentJobs : IPaymentJobs
    {
        private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;
        private readonly IRepositoryAsync<Payment> _paymentRepository;
        private readonly IRepositoryAsync<PaymentData> _paymentDataRepository;
        private readonly IPaymentService _paymentService;

        private HttpResponseMessage ResponseMessage { get; set; } = new HttpResponseMessage() { StatusCode = HttpStatusCode.NotAcceptable };
        private Payment? Payment { get; set; }
        private bool PaymentUpdatedInDB { get; set; } = false;
        private string ResultMessage { get; set; } = string.Empty;

        public PaymentJobs(IRepositoryAsync<Payment> paymentRepository, IRepositoryAsync<PaymentData> paymentDataRepository, IPaymentService paymentService)
        {
            _paymentRepository = paymentRepository;
            _paymentDataRepository = paymentDataRepository;
            _paymentService = paymentService;
            _retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotAcceptable)
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(3));
        }

        [DisplayName("Payment update inquiry: [Id: {0} | ExternalId: {1}")]
        public async Task<string> UpdatePaymentStatus(int id, string externalId)
        {
            await SendRequest(id, externalId);
            if (!ResponseMessage.IsSuccessStatusCode)
            {
                await CancelPayment();
            }
            return ResultMessage;
        }

        public async Task<HttpResponseMessage> SendRequest(int id, string externalId)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                Payment = await _paymentRepository.FirstOrDefaultAsync(new GetPaymentByIdSpecification(id));
                if (Payment is not null && Payment.PaymentStatusId == (int)EnumPaymentStatus.Requested)
                {
                    var externalResponse = await _paymentService.Status(externalId);
                    if (externalResponse.Success && externalResponse.Data.Status != "REQUEST")
                    {
                        await Update(externalResponse.Data);
                        ResultMessage = PaymentsMessages.UpdatedInJob();
                        ResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.Accepted };
                    }

                }
                else
                {
                    PaymentUpdatedInDB = true;
                    ResultMessage = PaymentsMessages.UpdatedInDB();
                    ResponseMessage = new HttpResponseMessage() { StatusCode = HttpStatusCode.Accepted };

                }
                return ResponseMessage;
            });
            return ResponseMessage;
        }

        private async Task Update(PaymentResponse externalResponse)
        {
            // Update the payment based on the response from the external service.
            if (Payment is not null)
            {
                Payment.PaymentStatusId = (int)EnumPaymentStatus.Confirmed;
                await _paymentRepository.UpdateAsync(Payment);
                await _paymentDataRepository.AddAsync(new PaymentData()
                {
                    CardBrand = externalResponse.CardBrand,
                    CardType = externalResponse.CardType,
                    LastFourDigits = externalResponse.LastFourDigits,
                    PaymentId = Payment.Id
                });
            }
        }
        private async Task CancelPayment()
        {
            if (Payment is not null)
            {
                var externalResponse = await _paymentService.Cancel(Payment.ExternalId);
                if (externalResponse.Success)
                {
                    Payment.PaymentStatusId = (int)EnumPaymentStatus.Undone;
                    await _paymentRepository.UpdateAsync(Payment);
                    ResultMessage = PaymentsMessages.Canceled();

                }
                else
                {
                    ResultMessage = PaymentsMessages.CanceledError();
                }

            }

        }
    }
}
