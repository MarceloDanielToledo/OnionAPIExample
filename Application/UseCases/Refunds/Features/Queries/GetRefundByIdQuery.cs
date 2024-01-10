using Application.Constants.Messages;
using Application.Interfaces;
using Application.UseCases.Refunds.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Refunds.Features.Queries
{
    public class GetRefundByIdQuery : IRequest<Response<Refund>>
    {
        public int Id { get; set; }
        public GetRefundByIdQuery(int id) => Id = id;
    }

    public class GetRefundByIdQueryHandler : IRequestHandler<GetRefundByIdQuery, Response<Refund>>
    {
        private readonly IRepositoryAsync<Refund> _repositoryAsync;

        public GetRefundByIdQueryHandler(IRepositoryAsync<Refund> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Refund>> Handle(GetRefundByIdQuery request, CancellationToken cancellationToken)
        {
            var refund = await _repositoryAsync.FirstOrDefaultAsync(new GetRefundByIdSpecification(request.Id), cancellationToken) ?? throw new KeyNotFoundException(ResultMessages.NotFoundError());
            return Response<Refund>.SuccessResponse(refund, ResultMessages.Success());
        }
    }
}
