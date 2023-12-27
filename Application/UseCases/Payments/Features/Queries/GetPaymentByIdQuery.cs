using Application.Constants.Messages;
using Application.Interfaces;
using Application.UseCases.Payments.Specifications;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Payments.Features.Queries
{
    public class GetPaymentByIdQuery : IRequest<Response<Payment>>
    {
        public int Id { get; set; }
        public GetPaymentByIdQuery(int id) => Id = id;
    }

    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Response<Payment>>
    {
        private readonly IRepositoryAsync<Payment> _repositoryAsync;

        public GetPaymentByIdQueryHandler(IRepositoryAsync<Payment> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<Payment>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _repositoryAsync.FirstOrDefaultAsync(new GetPaymentByIdSpecification(request.Id), cancellationToken) ?? throw new KeyNotFoundException(ResultMessages.NotFoundError());
            return Response<Payment>.SuccessResponse(payment, ResultMessages.Success());
        }
    }



}
