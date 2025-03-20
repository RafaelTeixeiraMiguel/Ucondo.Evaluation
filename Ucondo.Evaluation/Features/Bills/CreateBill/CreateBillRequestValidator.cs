using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.API.Features.Bills.CreateBill
{
    public class CreateBillRequestValidator : AbstractValidator<CreateBillRequest>
    {
        public CreateBillRequestValidator()
        {
            RuleFor(bill => bill.Code).SetValidator(new CodeValidator());
            RuleFor(bill => bill.Name).SetValidator(new NameValidator());
        }
    }
}
