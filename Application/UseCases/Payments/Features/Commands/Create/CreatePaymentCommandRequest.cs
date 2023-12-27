namespace Application.UseCases.Payments.Features.Commands.Create
{
    public class CreatePaymentCommandRequest
    {
        public int TerminalId { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }

    }
}
