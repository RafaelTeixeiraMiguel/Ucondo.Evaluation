using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Common.Validation;

namespace Ucondo.Evaluation.Application.Bills.UpdateBill
{
    public class UpdateBillCommand : IRequest<UpdateBillResult>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public string Type { get; set; }
        public Guid? ParentBillId { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new UpdateBillCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
