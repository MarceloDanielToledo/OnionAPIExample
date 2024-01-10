using Application.Constants.Messages;
using Application.Exceptions;
using Application.Interfaces;
using Application.UseCases.Payments.Constants.Enums;
using Application.UseCases.Payments.Constants.Messages;
using Application.UseCases.Refunds.Constants.Enums;
using Application.UseCases.Refunds.Constants.Messages;
using Application.UseCases.Refunds.Specifications;
using Application.Wrappers;
using Domain.Entities;
using ExternalServiceCommunication.Exceptions;
using ExternalServiceCommunication.Services;
using ExternalServiceCommunication.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Refunds.Features.Commands.Cancel
{
    public class CancelRefundCommand : IRequest<Response<Refund>>
    {
        public int Id { get; set; }
        public CancelRefundCommand(int id) => Id = id;
    }

    public class CancelRefundCommandHandler : IRequestHandler<CancelRefundCommand, Response<Refund>>
    {
        private readonly IRepositoryAsync<Refund> _repositoryAsync;
        private readonly IRefundsService _refundsService;

        public CancelRefundCommandHandler(IRepositoryAsync<Refund> repositoryAsync, IRefundsService refundsService)
        {
            _repositoryAsync = repositoryAsync;
            _refundsService = refundsService;
        }
        public async Task<Response<Refund>> Handle(CancelRefundCommand command, CancellationToken cancellationToken)
        {
            var refund = await _repositoryAsync.FirstOrDefaultAsync(new GetRefundByIdSpecification(command.Id), cancellationToken) ?? throw new KeyNotFoundException(ResultMessages.NotFoundError());
            if (refund.RefundStatusId != (int)EnumRefundStatus.Requested)
            {
                throw new ValidationException(RefundsMessages.CanceledErrorIncorrectStatus());
            }
            else
            {
                var externalResponse = await _refundsService.Cancel(refund.ExternalId);
                if (!externalResponse.Success)
                {
                    throw new ExternalServiceException(externalResponse.CodeState, externalResponse.Message);

                }
                else
                {
                    refund.RefundStatusId = (int)EnumRefundStatus.Cancelled;
                    await _repositoryAsync.UpdateAsync(refund, cancellationToken);
                    return Response<Refund>.SuccessResponse(refund, PaymentsMessages.Canceled());

                }


            }




        }
    }
}
