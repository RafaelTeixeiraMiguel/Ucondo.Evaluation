﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucondo.Evaluation.Application.Bills.DeleteBill
{
    public class DeleteBillCommandValidator : AbstractValidator<DeleteBillCommand>
    {
        public DeleteBillCommandValidator() { }
    }
}
