using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Common.Validation;

namespace Ucondo.Evaluation.Application.Bills.GetSuggestedCode
{
    public class GetSuggestedCodeCommand : IRequest<GetSuggestedCodeResult>
    {
        public Guid ParentId { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new GetSuggestedCodeCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
