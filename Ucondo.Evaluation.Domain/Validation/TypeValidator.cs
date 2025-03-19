using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Enums;

namespace Ucondo.Evaluation.Domain.Validation
{
    public class TypeValidator : AbstractValidator<BillType>
    {
        public TypeValidator()
        {
            RuleFor(code => code)
            .Must(MatchParentBill)
            .WithMessage("The type does not match parent's bill");
        }

        private bool MatchParentBill(BillType type)
        {
            return true;
        }
    }
}
