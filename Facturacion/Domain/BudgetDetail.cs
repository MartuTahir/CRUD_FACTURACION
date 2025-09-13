using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain
{
    public class BudgetDetail
    {
        public int Id { get; set; }
        public Article Article { get; set; }
        public int Count { get; set; }
    }
}
