using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucondo.Evaluation.Application.Bills.DeleteBill
{
    public class DeleteBillCommand : IRequest<DeleteBillResult>
    {
        public Guid Id { get; }

        public DeleteBillCommand(Guid id)
        {
            Id = id;
        }
    }
}
