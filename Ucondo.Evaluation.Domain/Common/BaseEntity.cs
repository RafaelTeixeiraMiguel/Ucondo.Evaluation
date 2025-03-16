using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Common.Validation;
using Validator = Ucondo.Evaluation.Common.Validation.Validator;

namespace Ucondo.Evaluation.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
        {
            return Validator.ValidateAsync(this);
        }
    }
}
