using FluentValidation;

namespace Application.Features.Payments.Commands.Create
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommandRequest>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.TerminalId)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} must be greater than 0.");
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("The {PropertyName} must be greater than 0.");
            RuleFor(x => x.Details)
                .NotNull()
                .WithMessage("The payment detail must not be empty.")
                .MaximumLength(250)
                .WithMessage("The payment reason must not exceed 100 characters.");

        }
    }
}
