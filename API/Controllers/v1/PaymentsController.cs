using Application.UseCases.Payments.Features.Commands.Cancel;
using Application.UseCases.Payments.Features.Commands.Create;
using Application.UseCases.Payments.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PaymentsController : BaseApiController
    {
        /// <summary>
        /// Create a new payment.
        /// </summary>
        /// <param name="request">Request body.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommandRequest request)
        {
            return CreatedAtAction(nameof(Create), await Mediator.Send(new CreatePaymentCommand(request)));
        }
        /// <summary>
        /// Cancel a payment
        /// </summary>
        /// <param name="id">Id</param>
        [HttpPut("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            return Ok(await Mediator.Send(new CancelPaymentCommand(id)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetPaymentByIdQuery(id)));
        }
    }
}
