using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.Application.Bills.CreateBill
{
    public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateBillCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Code: Must be unique;Range of code must be between 1 and 999; (using CodeValidator)
        /// - Type: Required if child bill;Must be the same as parent's; (using TypeValidator)
        /// </remarks>
        public CreateBillCommandValidator()
        {
            RuleFor(bill => bill.Code).SetValidator(new CodeValidator());
            RuleFor(bill => bill.Type).SetValidator(new TypeValidator());
        }
    }
}
