using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.Tests.Domain.Validation
{
    public class NameValidatorTest
    {
        private readonly NameValidator _validator;
        public NameValidatorTest()
        {
            _validator = new NameValidator();
        }

        [Fact(DisplayName = "Valid name should pass validations")]
        public void Given_ValidName_When_Validated_Then_ShouldNotHaveErrors()
        {
            var code = "This is a valid name";

            var result = _validator.TestValidate(code);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Empty name should fail validation")]
        public void Given_EmptyName_When_Validated_Then_ShouldHaveErrors()
        {
            string code = "";

            var result = _validator.TestValidate(code);

            result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("The name must not be empty.");
        }
    }
}
