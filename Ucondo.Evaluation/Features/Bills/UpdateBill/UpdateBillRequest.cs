using Ucondo.Evaluation.Domain.Enums;

namespace Ucondo.Evaluation.API.Features.Bills.UpdateBill
{
    public class UpdateBillRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public BillType Type { get; set; }
        public Guid? ParentBillId { get; set; }
    }
}
