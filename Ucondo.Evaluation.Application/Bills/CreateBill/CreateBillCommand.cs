using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Common.Validation;

namespace Ucondo.Evaluation.Application.Bills.CreateBill
{
    /// <summary>
    /// Command for creating a new bill.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a bill.
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateBillResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CreateBillCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateBillCommand : IRequest<CreateBillResult>
    {
        /// <summary>
        /// Gets or sets the bill code. Must be unique and meet format requirements
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets if the bill allow payments
        /// </summary>
        public bool AllowPayments { get; set; }
        /// <summary>
        /// Gets or sets bill's type. Must be the same as the parent account if any
        /// </summary>
        public string Type { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateBillCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
