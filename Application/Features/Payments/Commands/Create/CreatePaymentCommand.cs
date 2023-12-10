using Application.Constants.Messages;
using Application.Interfaces;
using Application.Specifications.Terminals;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Payments.Commands.Create
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

            public CreatePaymentCommandHandler(IRepositoryAsync<Payment> paymentRepository, IRepositoryAsync<Terminal> terminalRepository, IPaymentService paymentService)
            {
                _paymentRepository = paymentRepository;
                _terminalRepository = terminalRepository;
                _paymentService = paymentService;
            }

            public async Task<Response<Payment>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
            {
                var terminal = await _terminalRepository.FirstOrDefaultAsync(new GetTerminalByIdSpecification(command.Request.TerminalId)) ?? throw new KeyNotFoundException(TerminalMessages.NotFound());

                return Response<Payment>.SuccessResponse(new Payment(), "");
            }
        }

    }
}
