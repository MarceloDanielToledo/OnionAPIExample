namespace Application.UseCases.Refunds.Features.Commands.Create
{
    public class CreateRefundCommandRequest
    {
        public int PaymentId { get; set; }
        public string Reason { get; set; }
    }
}
