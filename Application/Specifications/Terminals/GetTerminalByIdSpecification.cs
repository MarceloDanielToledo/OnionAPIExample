using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Terminals
{
    public class GetTerminalByIdSpecification : Specification<Terminal>
    {
        public GetTerminalByIdSpecification(int id)
        {
            Query.Where(x => x.Id == id).AsNoTracking();

        }


    }
}
