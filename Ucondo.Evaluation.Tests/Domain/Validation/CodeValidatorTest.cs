using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.Tests.Domain.Validation
{
    public class CodeValidatorTest
    {
        private readonly CodeValidator _validator;
        public CodeValidatorTest()
        {
            _validator = new CodeValidator();
        }

        [Fact(DisplayName = "Valid code should pass validations")]
        public void Given_ValidCode_When_Validated_Then_ShouldNotHaveErrors()
        {
            var code = "1.2.3";

            var result = _validator.TestValidate(code);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Empty code should fail validation")]
        public void Given_EmptyCode_When_Validated_Then_ShouldHaveErrors()
        {
            string code = "";

            var result = _validator.TestValidate(code);

            result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("The code must not be empty.");
        }

        [Fact(DisplayName = "Code with wrong format should fail validation")]
        public void Given_WrongFormatCode_When_Validated_Then_ShouldHaveErrors()
        {
            string code = "InvalidFormat";

            var result = _validator.TestValidate(code);

            result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("The code must follow the correct pattern (n.n.n...).");
        }

        [Fact(DisplayName = "Code with invalid range should fail validation")]
        public void Given_WrongRangeCode_When_Validated_Then_ShouldHaveErrors()
        {
            string code = "1.1000";

            var result = _validator.TestValidate(code);

            result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("The code sequencial number must be between 1 and 999.");
        }
    }
}
