using AutoMapper;
using Ucondo.Evaluation.Domain.Entities;

namespace Ucondo.Evaluation.Application.Bills.UpdateBill
{
    public class UpdateBillProfile : Profile
    {
        public UpdateBillProfile()
        {
            CreateMap<UpdateBillCommand, Bill>();
            CreateMap<Bill, UpdateBillResult>();
        }
    }
}
