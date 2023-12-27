using Application.Interfaces;
using Application.UseCases.Payments.Constants.Enums;
using Application.UseCases.Payments.Constants.Messages;
using Application.UseCases.Payments.Jobs;
using Application.UseCases.Terminals.Constants.Messages;
using Application.UseCases.Terminals.Specifications;
using Application.Wrappers;
using Domain.Entities;
using ExternalServiceCommunication.Exceptions;
using Hangfire;
using MediatR;

namespace Application.UseCases.Payments.Features.Commands.Create
{
    public class CreatePaymentCommand : IRequest<Response<Payment>>
    {
        public CreatePaymentCommandRequest Request { get; set; }

        public CreatePaymentCommand(CreatePaymentCommandRequest request)
        {
            Request = request;
        }

        public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Response<Payment>>
        {

            private readonly IRepositoryAsync<Payment> _paymentRepository;
            private readonly IRepositoryAsync<Terminal> _terminalRepository;
            private readonly IPaymentService _paymentService;
            private readonly IPaymentJobs _paymentJobs;

            public CreatePaymentCommandHandler(
                IRepositoryAsync<Payment> paymentRepository, 
                IRepositoryAsync<Terminal> terminalRepository, 
                IPaymentService paymentService, 
                IPaymentJobs paymentJobs)
            {
                _paymentRepository = paymentRepository;
                _terminalRepository = terminalRepository;
                _paymentService = paymentService;
                _paymentJobs = paymentJobs;
            }

            public async Task<Response<Payment>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
            {
                var terminal = await _terminalRepository.FirstOrDefaultAsync(new GetTerminalByIdSpecification(command.Request.TerminalId), cancellationToken) ?? throw new KeyNotFoundException(TerminalsMessages.NotFound());
                var externalResponse = await _paymentService.Create(terminal.ExternalId, command.Request.Amount);
                if (!externalResponse.Success)
                {
                    throw new ExternalServiceException(externalResponse.CodeState, externalResponse.Message);
                }
                else
                {
                    var NewPayment = await _paymentRepository.AddAsync(new Payment
                    {
                        Amount = command.Request.Amount,
                        Details = command.Request.Details,
                        ExternalId = externalResponse.Data.PaymentId,
                        PaymentStatusId = (int)EnumPaymentStatus.Requested,
                        TerminalId = terminal.Id
                    }, cancellationToken);
                    BackgroundJob.Enqueue(() => _paymentJobs.UpdatePaymentStatus(NewPayment.Id, NewPayment.ExternalId));
                    return Response<Payment>.SuccessResponse(NewPayment, PaymentsMessages.RequestCreated());

                }


            }
        }

    }
}
