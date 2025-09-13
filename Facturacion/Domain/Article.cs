using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain
{
    public class Article
    {
        public int IdArticle { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return IdArticle + " " + Name + " " + Price + " " + Active;
        }

    }
}
