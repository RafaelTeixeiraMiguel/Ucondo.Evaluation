using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucondo.Evaluation.Domain.Validation
{
    public class TypeValidator : AbstractValidator<string>
    {
        public TypeValidator()
        {
            RuleFor(code => code)
            .Must(MatchParentBill)
            .WithMessage("The type does not match parent's bill");
        }

        private bool MatchParentBill(string type)
        {
            return true;
        }
    }
}
