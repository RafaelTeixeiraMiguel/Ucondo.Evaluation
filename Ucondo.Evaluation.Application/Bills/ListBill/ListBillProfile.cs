using AutoMapper;
using Ucondo.Evaluation.Domain.Entities;

namespace Ucondo.Evaluation.Application.Bills.ListBill
{
    public class ListBillProfile : Profile
    {
        public ListBillProfile()
        {
            CreateMap<IEnumerable<Bill>, ListBillResult>();
            CreateMap<Bill, BillResult>();
        }
    }
}
