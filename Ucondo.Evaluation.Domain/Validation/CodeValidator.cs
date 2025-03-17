using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucondo.Evaluation.Domain.Validation
{
    public class CodeValidator : AbstractValidator<string>
    {
        public CodeValidator()
        {
            RuleFor(code => code)
            .NotNull()
            .WithMessage("The code must not be null.")
            .NotEmpty()
            .WithMessage("The code must not be empty.");
        }
    }
}
