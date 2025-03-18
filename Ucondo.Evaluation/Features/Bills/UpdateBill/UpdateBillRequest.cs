namespace Ucondo.Evaluation.API.Features.Bills.UpdateBill
{
    public class UpdateBillRequest
    {
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public string Type { get; set; }
        public Guid? ParentBillId { get; set; }
    }
}
