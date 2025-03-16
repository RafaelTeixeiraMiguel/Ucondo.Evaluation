using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Common;

namespace Ucondo.Evaluation.Domain.Entities
{
    public class Conta : BaseEntity
    {
        public string Codigo { get; set; }
        public bool AceitaLancamentos { get; set; }
        public string Tipo { get; set; }
    }
}
