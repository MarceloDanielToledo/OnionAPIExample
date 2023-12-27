using Ardalis.Specification;
using Domain.Entities;

namespace Application.UseCases.Terminals.Specifications
{
    public class GetTerminalByIdSpecification : Specification<Terminal>
    {
        public GetTerminalByIdSpecification(int id)
        {
            Query.Where(x => x.Id == id).AsNoTracking();

        }


    }
}
