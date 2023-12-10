namespace Application.Features.Payments.Commands.Create
{
    public class CreatePaymentCommandRequest
    {
        public int TerminalId { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }

    }
}
