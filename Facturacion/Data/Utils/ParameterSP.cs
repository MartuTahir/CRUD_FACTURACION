using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Data.Utils
{
    public class ParameterSP
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;
        public int Size { get; set; } = 0;

        public SqlDbType Type { get; set; }
    }
}
