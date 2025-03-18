using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucondo.Evaluation.Application.Bills.ListBill
{
    public class ListBillCommandValidator : AbstractValidator<ListBillCommand>
    {
        public ListBillCommandValidator() { }
    }
}
