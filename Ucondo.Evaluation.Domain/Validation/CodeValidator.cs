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
            .WithMessage("The code must not be empty.")
            .Matches(@"^(\d+)(\.\d+)*$")
            .WithMessage("The code must follow the correct pattern (n.n.n...).")
            .Must(BeValidSequencialNumber)
            .WithMessage("The code sequencial number must be between 1 and 999.");
        }

        private bool BeValidSequencialNumber(string code)
        {
            var codeParts = code.Split('.');
            var lastSegmentStr = codeParts.Last();

            int.TryParse(lastSegmentStr, out int lastSegment);

            return !(lastSegment > 999 || lastSegment < 1);
        }
    }
}
