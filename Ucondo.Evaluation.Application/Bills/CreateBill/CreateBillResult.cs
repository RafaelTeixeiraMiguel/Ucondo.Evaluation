using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucondo.Evaluation.Application.Bills.CreateBill
{
    public class CreateBillResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly created bill.
        /// </summary>
        /// <value>A GUID that uniquely identifies the created user in the system.</value>
        public Guid Id { get; set; }
    }
}
