using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Common;
using Ucondo.Evaluation.Domain.Enums;

namespace Ucondo.Evaluation.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public required string Code { get; set; }
        public bool AllowPayments { get; set; }
        public BillType Type { get; set; }
        public Guid? ParentBillId { get; set; }
        public Bill? ParentBill { get; set; }
    }
}
