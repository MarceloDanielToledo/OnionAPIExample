using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.Payments.Specifications
{
    public class GetPaymentByIdSpecification : Specification<Payment>
    {
        public GetPaymentByIdSpecification(int id)
        {
            Query.Where(x => x.Id == id);

        }
    }
}
