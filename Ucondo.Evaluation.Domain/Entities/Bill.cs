using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Common;

namespace Ucondo.Evaluation.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public string Code { get; set; }
        public bool AllowPayments { get; set; }
        public string Type { get; set; }
    }
}
