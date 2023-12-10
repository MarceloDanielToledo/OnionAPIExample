using Application.Features.Payments.Commands.Create;
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
    }
}
