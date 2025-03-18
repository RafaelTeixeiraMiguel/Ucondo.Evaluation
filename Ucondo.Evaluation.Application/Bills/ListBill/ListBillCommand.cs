using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application.Bills.CreateBill;

namespace Ucondo.Evaluation.Application.Bills.ListBill
{
    public class ListBillCommand : IRequest<ListBillResult>
    {
        public ListBillCommand() { }
    }
}
