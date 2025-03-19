using FluentValidation;
using Ucondo.Evaluation.API.Features.Bills.ListBill;

namespace Ucondo.Evaluation.API.Features.Bills.GetSuggestedCode
{
    public class GetSuggestedCodeValidator : AbstractValidator<GetSuggestedCodeRequest>
    {
        public GetSuggestedCodeValidator()
        {
            RuleFor(parentId => parentId)
            .NotNull()
            .WithMessage("The parent id must not be null.")
            .NotEmpty()
            .WithMessage("The parent id must not be empty.");
        }
    }
}
