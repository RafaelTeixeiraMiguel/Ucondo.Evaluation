using FluentValidation;
using System.Threading;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Domain.Validation
{
    public class ParentBillValidator : AbstractValidator<Bill>
    {
        private IBillRepository _billRepository;
        public ParentBillValidator(IBillRepository billRepository)
        {
            _billRepository = billRepository;
            RuleFor(bill => bill)
            .Must(ParentBillShouldNotAllowPayments)
            .WithMessage("Parent bill must not allow payments")
            .Must(ParentTypeBeTheSameAsBillType)    
            .WithMessage("Bill type must be the same as parent's type")
            .Must(BillCodeMustContinueParentCode)
            .WithMessage("Bill code must be a child from parent's code");
        }

        private bool ParentBillShouldNotAllowPayments(Bill bill)
        {
            var parentBill = _billRepository.GetByIdAsync((Guid)bill.ParentBillId).Result;

            return !parentBill.AllowPayments;
        }

        private bool ParentTypeBeTheSameAsBillType(Bill bill)
        {
            var parentBill = _billRepository.GetByIdAsync((Guid)bill.ParentBillId).Result;

            return parentBill.Type == bill.Type;
        }

        private bool BillCodeMustContinueParentCode(Bill bill)
        {
            var parentBill = _billRepository.GetByIdAsync((Guid)bill.ParentBillId).Result;

            return bill.Code.StartsWith(parentBill.Code);
        }
    }
}
