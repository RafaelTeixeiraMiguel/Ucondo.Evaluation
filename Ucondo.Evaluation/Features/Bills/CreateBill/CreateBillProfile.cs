using AutoMapper;
using Ucondo.Evaluation.Application.Bills.CreateBill;

namespace Ucondo.Evaluation.API.Features.Bills.CreateBill
{
    public class CreateBillProfile : Profile
    {
        public CreateBillProfile()
        {
            CreateMap<CreateBillRequest, CreateBillCommand>();
            CreateMap<CreateBillResult, CreateBillResponse>();
        }
    }
}
