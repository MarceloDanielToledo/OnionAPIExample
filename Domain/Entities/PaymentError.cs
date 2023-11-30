using Domain.Common;

namespace Domain.Entities
{
    public class PaymentError : BaseEntity
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public int ErrorId { get; set; }
        public Error Error { get; set; }
    }
}
