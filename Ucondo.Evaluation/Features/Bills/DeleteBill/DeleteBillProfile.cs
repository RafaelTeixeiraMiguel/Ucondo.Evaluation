using AutoMapper;
using Ucondo.Evaluation.Application.Bills.DeleteBill;

namespace Ucondo.Evaluation.API.Features.Bills.DeleteBill
{
    public class DeleteBillProfile : Profile
    {
        public DeleteBillProfile()
        {
            CreateMap<DeleteBillRequest, DeleteBillCommand>();
            CreateMap<DeleteBillResult, DeleteBillResponse>();
        }
    }
}
