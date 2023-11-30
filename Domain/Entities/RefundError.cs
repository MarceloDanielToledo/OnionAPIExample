using Domain.Common;

namespace Domain.Entities
{
    public class RefundError : BaseEntity
    {
        public int RefundId { get; set; }
        public Refund Refund { get; set; }
        public int ErrorId { get; set; }
        public Error Error { get; set; }
    }
}
