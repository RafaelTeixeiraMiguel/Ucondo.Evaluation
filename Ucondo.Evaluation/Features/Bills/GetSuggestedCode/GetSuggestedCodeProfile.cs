using AutoMapper;
using Ucondo.Evaluation.Application.Bills.GetSuggestedCode;

namespace Ucondo.Evaluation.API.Features.Bills.GetSuggestedCode
{
    public class GetSuggestedCodeProfile : Profile
    {
        public GetSuggestedCodeProfile()
        {
            CreateMap<GetSuggestedCodeRequest, GetSuggestedCodeCommand>();
            CreateMap<GetSuggestedCodeResult, GetSuggestedCodeResponse>();
        }
    }
}
