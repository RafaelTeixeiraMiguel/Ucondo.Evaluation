using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;

namespace Ucondo.Evaluation.Application.Bills.CreateBill
{
    public class CreateBillProfile : Profile
    {
        public CreateBillProfile()
        {
            CreateMap<CreateBillCommand, Bill>();
            CreateMap<Bill, CreateBillResult>();
        }
    }
}
