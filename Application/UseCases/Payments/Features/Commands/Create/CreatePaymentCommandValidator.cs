using FluentValidation;

namespace Application.UseCases.Payments.Features.Commands.Create
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.Request.TerminalId)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} must be greater than 0.");
            RuleFor(x => x.Request.Amount)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} must be greater than 0.");
            RuleFor(x => x.Request.Details)
                .NotNull()
                .WithMessage("The payment detail must not be empty.")
                .MaximumLength(100)
                .WithMessage("The payment detail must not exceed 100 characters.");

        }
    }
}
