using FluentValidation.TestHelper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;
using Ucondo.Evaluation.Domain.Repositories;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.Tests.Domain.Validation
{
    public class ParentBillValidatorTest
    {
        private readonly ParentBillValidator _validator;
        private readonly IBillRepository _billRepository;
        public ParentBillValidatorTest()
        {

            _billRepository = Substitute.For<IBillRepository>();
            _validator = new ParentBillValidator(_billRepository);
        }

        [Fact(DisplayName = "Valid parent should pass validations")]
        public void Given_ValidParent_When_Validated_Then_ShouldNotHaveErrors()
        {
            var parentBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1",
                Name = "Test",
                AllowPayments = false,
                ParentBill = null,
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var childBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1.1",
                Name = "Child test",
                AllowPayments = true,
                ParentBill = parentBill,
                ParentBillId = parentBill.Id,
                Type = BillType.Despesa
            };

            _billRepository.GetByIdAsync(parentBill.Id)
            .Returns(parentBill);

            var result = _validator.TestValidate(childBill);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Invalid parent that allow payments should not pass validations")]
        public void Given_InvalidParentAllowPayments_When_Validated_Then_ShouldNotHaveErrors()
        {
            var parentBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1",
                Name = "Test",
                AllowPayments = true,
                ParentBill = null,
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var childBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1.1",
                Name = "Child test",
                AllowPayments = true,
                ParentBill = parentBill,
                ParentBillId = parentBill.Id,
                Type = BillType.Despesa
            };

            _billRepository.GetByIdAsync(parentBill.Id)
            .Returns(parentBill);

            var result = _validator.TestValidate(childBill);

            result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Parent bill must not allow payments");
        }

        [Fact(DisplayName = "Invalid parent with different type should not pass validations")]
        public void Given_InvalidParentDifferentType_When_Validated_Then_ShouldNotHaveErrors()
        {
            var parentBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1",
                Name = "Test",
                AllowPayments = false,
                ParentBill = null,
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var childBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1.1",
                Name = "Child test",
                AllowPayments = true,
                ParentBill = parentBill,
                ParentBillId = parentBill.Id,
                Type = BillType.Receita
            };

            _billRepository.GetByIdAsync(parentBill.Id)
            .Returns(parentBill);

            var result = _validator.TestValidate(childBill);

            result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Bill type must be the same as parent's type");
        }

        [Fact(DisplayName = "Invalid parent with different code should not pass validations")]
        public void Given_InvalidParentDifferentCode_When_Validated_Then_ShouldNotHaveErrors()
        {
            var parentBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1",
                Name = "Test",
                AllowPayments = false,
                ParentBill = null,
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var childBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "2.1",
                Name = "Child test",
                AllowPayments = true,
                ParentBill = parentBill,
                ParentBillId = parentBill.Id,
                Type = BillType.Despesa
            };

            _billRepository.GetByIdAsync(parentBill.Id)
            .Returns(parentBill);

            var result = _validator.TestValidate(childBill);

            result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Bill code must be a child from parent's code");
        }
    }
}
