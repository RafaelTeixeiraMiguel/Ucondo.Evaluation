using Ucondo.Evaluation.Domain.Enums;

namespace Ucondo.Evaluation.API.Features.Bills.ListBill
{
    public class ListBillRequest
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool? AllowPayments { get; set; }
        public BillType? Type { get; set; }
        public Guid? ParentBillId { get; set; }

        public List<BillType>? Types { get; set; }
        public string? CodeContains { get; set; }
        public bool? HasParent { get; set; }

        public string? OrderBy { get; set; }
        public bool Desc { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
