using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.Payments.Constants.Enums;
using Application.UseCases.Payments.Constants.Messages;
using Application.UseCases.Payments.Specifications;
using Application.Wrappers;
using Domain.Entities;
using ExternalServiceCommunication.Exceptions;
using MediatR;

namespace Application.UseCases.Payments.Features.Commands.Cancel
{
    public class CancelPaymentCommand : IRequest<Response<Payment>>
    {
        public int Id { get; set; }
        public CancelPaymentCommand(int id) => Id = id;
    }
    public class CancelPaymentCommandHandler : IRequestHandler<CancelPaymentCommand, Response<Payment>>
    {
        private readonly IRepositoryAsync<Payment> _paymentRepository;
        private readonly IPaymentService _paymentService;

        public CancelPaymentCommandHandler(IRepositoryAsync<Payment> paymentRepository, IPaymentService paymentService)
        {
            _paymentRepository = paymentRepository;
            _paymentService = paymentService;
        }


        public async Task<Response<Payment>> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.FirstOrDefaultAsync(new GetPaymentByIdSpecification(request.Id), cancellationToken) ?? throw new KeyNotFoundException(PaymentsMessages.NotFound());
            if (payment.PaymentStatusId != (int)EnumPaymentStatus.Requested)
            {
                throw new ValidationException(PaymentsMessages.CanceledErrorIncorrectStatus());
            }
            else
            {
                var externalResponse = await _paymentService.Cancel(payment.ExternalId);
                if (!externalResponse.Success)
                {
                    throw new ExternalServiceException(externalResponse.CodeState, externalResponse.Message);

                }
                else
                {
                    payment.PaymentStatusId = (int)EnumPaymentStatus.Cancelled;
                    await _paymentRepository.UpdateAsync(payment, cancellationToken);
                    return Response<Payment>.SuccessResponse(payment, PaymentsMessages.Canceled());

                }


            }
        }
    }


}
