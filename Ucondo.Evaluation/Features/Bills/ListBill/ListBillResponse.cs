namespace Ucondo.Evaluation.API.Features.Bills.ListBill
{
    public class ListBillResponse
    {
        public List<BillResponse> Items { get; set; }
    }

    public class BillResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public string Type { get; set; }
    }
}
