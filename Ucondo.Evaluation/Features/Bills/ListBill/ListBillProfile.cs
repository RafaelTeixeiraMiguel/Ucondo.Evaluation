using AutoMapper;
using Ucondo.Evaluation.Application.Bills.ListBill;
using Ucondo.Evaluation.Domain.Entities;

namespace Ucondo.Evaluation.API.Features.Bills.ListBill
{
    public class ListBillProfile : Profile
    {
        public ListBillProfile()
        {
            CreateMap<ListBillRequest, ListBillCommand>();
            CreateMap<ListBillResult, ListBillResponse>();
            CreateMap<List<Bill>, ListBillResult>().ForMember(dest => dest.Bills, opt => opt.MapFrom(src => src));
        }
    }
}
