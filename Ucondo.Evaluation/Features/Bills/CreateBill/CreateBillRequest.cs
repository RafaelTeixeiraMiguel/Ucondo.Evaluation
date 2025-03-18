using Ucondo.Evaluation.Domain.Entities;

namespace Ucondo.Evaluation.API.Features.Bills.CreateBill
{
    public class CreateBillRequest
    {
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public string Type { get; set; }
        public Guid? ParentBillId { get; set; }
    }
}
