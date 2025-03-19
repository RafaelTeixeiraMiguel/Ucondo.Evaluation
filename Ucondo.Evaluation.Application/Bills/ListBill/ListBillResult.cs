using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;

namespace Ucondo.Evaluation.Application.Bills.ListBill
{
    public class ListBillResult
    {
        public List<BillResult> Bills { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class BillResult
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public BillType Type { get; set; }
        public Bill? ParentBill { get; set; }
    }
}
