using Application.UseCases.Refunds.Features.Commands.Cancel;
using Application.UseCases.Refunds.Features.Commands.Create;
using Application.UseCases.Refunds.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class RefundsController : BaseApiController
    {
        /// <summary>
        /// Create a new refund.
        /// </summary>
        /// <param name="request">Request body.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRefundCommandRequest request)
        {
            return CreatedAtAction(nameof(Create), await Mediator.Send(new CreateRefundCommand(request)));
        }

        /// <summary>
        /// Cancel a refund
        /// </summary>
        /// <param name="id">Id</param>
        [HttpPut("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            return Ok(await Mediator.Send(new CancelRefundCommand(id)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetRefundByIdQuery(id)));
        }

    }
}
