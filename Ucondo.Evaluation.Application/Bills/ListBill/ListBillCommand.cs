using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Common.Validation;
using Ucondo.Evaluation.Domain.Enums;

namespace Ucondo.Evaluation.Application.Bills.ListBill
{
    public class ListBillCommand : IRequest<ListBillResult>
    {
        public string? Code { get; set; }
        public bool? AllowPayments { get; set; }
        public BillType? Type { get; set; }
        public Guid? ParentBillId { get; set; }

        public List<BillType>? Types { get; set; }
        public string? CodeContains { get; set; }
        public bool? HasParent { get; set; }

        public string? OrderBy { get; set; }
        public bool Desc { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public ValidationResultDetail Validate()
        {
            var validator = new ListBillCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
