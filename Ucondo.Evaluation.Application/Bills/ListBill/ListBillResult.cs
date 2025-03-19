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
        public List<BillResult> Sales { get; set; }
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
