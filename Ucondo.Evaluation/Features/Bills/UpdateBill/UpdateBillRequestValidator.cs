using FluentValidation;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.API.Features.Bills.UpdateBill
{
    public class UpdateBillRequestValidator : AbstractValidator<UpdateBillRequest>
    {
        public UpdateBillRequestValidator()
        {
            RuleFor(bill => bill.Code).SetValidator(new CodeValidator());
            RuleFor(bill => bill.Name).SetValidator(new NameValidator());
        }
    }
}
