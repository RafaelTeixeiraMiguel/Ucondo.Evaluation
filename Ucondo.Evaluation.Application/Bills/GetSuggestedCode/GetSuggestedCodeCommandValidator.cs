using FluentValidation;

namespace Ucondo.Evaluation.Application.Bills.GetSuggestedCode
{
    public class GetSuggestedCodeCommandValidator : AbstractValidator<GetSuggestedCodeCommand>
    {
        public GetSuggestedCodeCommandValidator()
        {
            RuleFor(parentId => parentId)
            .NotNull()
            .WithMessage("The parent id must not be null.")
            .NotEmpty()
            .WithMessage("The parent id must not be empty.");
        }
    }
}
