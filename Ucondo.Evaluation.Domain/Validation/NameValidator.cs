using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucondo.Evaluation.Domain.Validation
{
    public class NameValidator : AbstractValidator<string>
    {
        public NameValidator()
        {
            RuleFor(code => code)
            .NotNull()
            .WithMessage("The name must not be null.")
            .NotEmpty()
            .WithMessage("The name must not be empty.");
        }
    }
}
