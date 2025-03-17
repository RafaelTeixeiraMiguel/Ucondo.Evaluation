using AutoMapper;
using Ucondo.Evaluation.Application.Bills.CreateBill;

namespace Ucondo.Evaluation.API.Features.Bill
{
    /// <summary>
    /// Profile for mapping between Application and API CreateBill responses
    /// </summary>
    public class CreateBillProfile : Profile
    {
        public CreateBillProfile()
        {
            CreateMap<CreateBillRequest, CreateBillCommand>();
            CreateMap<CreateBillResult, CreateBillResponse>();
        }
    }
}
