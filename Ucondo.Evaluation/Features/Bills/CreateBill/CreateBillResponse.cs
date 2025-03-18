using Ucondo.Evaluation.Domain.Entities;

namespace Ucondo.Evaluation.API.Features.Bills.CreateBill
{
    public class CreateBillResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public string Type { get; set; }
        public Bill? ParentBill { get; set; }
    }
}
