using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.Refunds.Specifications
{
    public class GetRefundByPaymentIdSpecification : Specification<Refund>
    {
        public GetRefundByPaymentIdSpecification(int paymentId) 
        {
            Query.Where(x => x.PaymentId == paymentId);
        }
    }
}
