using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.API.Features.Bill
{
    public class CreateBillRequestValidator : AbstractValidator<CreateBillRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateBillRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Code: Must be unique;Range of code must be between 1 and 999; (using CodeValidator)
        /// - Type: Required if child bill;Must be the same as parent's; (using TypeValidator)
        /// </remarks>
        public CreateBillRequestValidator()
        {
            RuleFor(bill => bill.Code).SetValidator(new CodeValidator());
            RuleFor(bill => bill.Type).SetValidator(new TypeValidator());
        }
    }
}
