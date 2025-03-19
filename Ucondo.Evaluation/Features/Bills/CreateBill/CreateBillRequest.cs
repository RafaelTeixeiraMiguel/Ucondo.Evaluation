using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;

namespace Ucondo.Evaluation.API.Features.Bills.CreateBill
{
    public class CreateBillRequest
    {
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public BillType Type { get; set; }
        public Guid? ParentBillId { get; set; }
    }
}
