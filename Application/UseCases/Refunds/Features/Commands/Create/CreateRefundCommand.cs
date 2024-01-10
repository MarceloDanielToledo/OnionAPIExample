using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.Payments.Constants.Enums;
using Application.UseCases.Payments.Constants.Messages;
using Application.UseCases.Payments.Specifications;
using Application.UseCases.Refunds.Constants.Enums;
using Application.UseCases.Refunds.Constants.Messages;
using Application.UseCases.Refunds.Jobs;
using Application.UseCases.Refunds.Specifications;
using Application.Wrappers;
using Domain.Entities;
using ExternalServiceCommunication.Exceptions;
using ExternalServiceCommunication.Services.Interfaces;
using Hangfire;
using MediatR;

namespace Application.UseCases.Refunds.Features.Commands.Create
{
    public class CreateRefundCommand : IRequest<Response<Refund>>
    {
        public CreateRefundCommandRequest Request { get; set; }
        public CreateRefundCommand(CreateRefundCommandRequest request) => Request = request;
    }


    public class CreateRefundCommandHandler : IRequestHandler<CreateRefundCommand, Response<Refund>>
    {
        private readonly IRepositoryAsync<Payment> _paymentRepository;
        private readonly IRepositoryAsync<Refund> _refundRepository;
        private readonly IRefundsService _refundsService;
        private readonly IRefundJobs _refundJobs;

        public CreateRefundCommandHandler(IRepositoryAsync<Payment> paymentRepository, IRepositoryAsync<Refund> refundRepository, IRefundsService refundsService, IRefundJobs refundJobs)
        {
            _paymentRepository = paymentRepository;
            _refundRepository = refundRepository;
            _refundsService = refundsService;
            _refundJobs = refundJobs;
        }


        public async Task<Response<Refund>> Handle(CreateRefundCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.FirstOrDefaultAsync(new GetPaymentByIdSpecification(command.Request.PaymentId), cancellationToken) ?? throw new KeyNotFoundException(PaymentsMessages.NotFound());
            if (payment.PaymentStatusId != (int)EnumPaymentStatus.Confirmed)
            {
                throw new ValidationException(PaymentsMessages.RefundErrorIncorrectStatus());
            }

            var refund = await _refundRepository.FirstOrDefaultAsync(new GetRefundByPaymentIdSpecification(command.Request.PaymentId), cancellationToken);
            if (refund is not null)
            {
                throw new ValidationException(RefundsMessages.AlreadyExist());
            }

            var externalResponse = await _refundsService.Create(payment.ExternalId, command.Request.Reason);
            if (!externalResponse.Success)
            {
                throw new ExternalServiceException(externalResponse.CodeState, externalResponse.Message);
            }
            else
            {
                var newRefund = await _refundRepository.AddAsync(new Refund()
                {
                    ExternalId = externalResponse.Data.Id,
                    RefundStatusId = (int)EnumRefundStatus.Requested,
                    PaymentId = payment.Id,
                    Reason = command.Request.Reason,
                }, cancellationToken);
                BackgroundJob.Enqueue(() => _refundJobs.UpdateStatus(newRefund.Id, newRefund.ExternalId));
                return Response<Refund>.SuccessResponse(newRefund, RefundsMessages.Created());
            }
        }
    }

}
