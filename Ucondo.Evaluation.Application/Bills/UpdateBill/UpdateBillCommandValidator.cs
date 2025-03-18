using FluentValidation;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.Application.Bills.UpdateBill
{
    public class UpdateBillCommandValidator : AbstractValidator<UpdateBillCommand>
    {
        public UpdateBillCommandValidator()
        {
            RuleFor(bill => bill.Code).SetValidator(new CodeValidator());
            RuleFor(bill => bill.Type).SetValidator(new TypeValidator());
        }
    }
}
