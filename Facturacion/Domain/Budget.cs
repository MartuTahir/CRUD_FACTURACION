using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain
{
    public class Budget
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Client { get; set; }

        public PayMethod? PayMethod { get; set; }

        public bool Active { get; set; }

        public List<BudgetDetail> Details { get; set; } = new();

        public void AddDetail(BudgetDetail detail)
        {
            if (detail == null || detail.Article == null) return; //chequear que no este nada en null 

            // buscar si ya existe un detalle con el mismo artículo
            var existing = Details.FirstOrDefault(d => d.Article.IdArticle == detail.Article.IdArticle);
            //firstOrDefault para buscar en la lista si ya existe un articulo con el mismo id

            if (existing != null)
            {
                existing.Count += detail.Count;
            }
            else
            {
                // Si no existe, se agrega como nuevo
                Details.Add(detail);
            }
        }

        public void RemoveDetail(int index)
        {
            Details.RemoveAt(index);
        }

        public override string ToString()
        {
            return Id + " " + Date + " " + Client + " " + PayMethod;
        }

    }
}
