using AutoMapper;
using Ucondo.Evaluation.Application.Bills.UpdateBill;

namespace Ucondo.Evaluation.API.Features.Bills.UpdateBill
{
    public class UpdateBillProfile : Profile
    {
        public UpdateBillProfile()
        {
            CreateMap<UpdateBillRequest, UpdateBillCommand>();
            CreateMap<UpdateBillResult, UpdateBillResponse>();
        }
    }
}
