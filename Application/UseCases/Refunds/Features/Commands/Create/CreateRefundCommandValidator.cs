using FluentValidation;

namespace Application.UseCases.Refunds.Features.Commands.Create
{
    public class CreateRefundCommandValidator : AbstractValidator<CreateRefundCommand>
    {
        public CreateRefundCommandValidator()
        {
            RuleFor(x => x.Request.Reason)
                .NotNull()
                .WithMessage("The refund reason must not be empty.")
                .MaximumLength(100)
                .WithMessage("The refund reason must not exceed 100 characters.");
        }

    }
}
