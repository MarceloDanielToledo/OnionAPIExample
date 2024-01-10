using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.Refunds.Specifications
{
    public class GetRefundByIdSpecification : Specification<Refund>
    {
        public GetRefundByIdSpecification(int id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
