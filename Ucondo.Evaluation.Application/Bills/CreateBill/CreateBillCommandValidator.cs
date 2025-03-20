using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Repositories;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.Application.Bills.CreateBill
{
    public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
    {
        public CreateBillCommandValidator()
        {
            RuleFor(bill => bill.Code).SetValidator(new CodeValidator());
            RuleFor(bill => bill.Name).SetValidator(new NameValidator());
        }
    }
}
