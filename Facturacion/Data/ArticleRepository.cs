using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Data.Utils;
using Facturacion.Domain;
using Microsoft.Data.SqlClient;

namespace Facturacion.Data
{
    public class ArticleRepository : IArticleRepository
    {
        public bool Delete(int id)
        {
            //crear parametro
            List<ParameterSP> paramList = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    Name = "@id_articulo",
                    Value = id
                }
            };

            //conectar bd y traer registro
            int rowsAffected = DataHelper.GetInstance().ExecuteSPDML("SP_REGISTRAR_BAJA_ARTICULO", paramList);

            return rowsAffected > 0;
        }

        public List<Article> GetAll()
        {
            List<Article> list = new List<Article>();

            // conectar bd y traer registros
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS");

            //mapear resultados
            foreach(DataRow row in dt.Rows)
            {
                Article a = new Article();
                a.IdArticle = (int)row["id_articulo"];
                a.Name = (string)row["nombre"];
                a.Price = (int)row["pre_unitario"];
                a.Active = (bool)row["esta_activo"];
                list.Add(a);
            }
            return list;
        }

        public Article GetById(int id)
        {
            //crear parametro
            List<ParameterSP> paramList = new List<ParameterSP>()
            {
                new ParameterSP()
                {
                    Name = "@id_articulo",
                    Value = id,
                }
            };

            //conectar bd y traer registro
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULO_POR_CODIGO", paramList);

            //si las filas son > 0 ejecuta foreach para los atributos del registro
            if (dt != null && dt.Rows.Count > 0)
            {
                Article a = new Article()
                {
                    IdArticle = (int)dt.Rows[0]["id_articulo"],
                    Name = (string)dt.Rows[0]["nombre"],
                    Price = (int)dt.Rows[0]["pre_unitario"],
                    Active = (bool)dt.Rows[0]["esta_activo"]
                };
                return a;
            }
            else
            {
                return null;
            }
        }

        public bool Save(Article article)
        {
            try
            {
                List<ParameterSP> paramList = new List<ParameterSP>()
                {
                    new ParameterSP { Name = "@id_articulo", 
                                        Value = article.IdArticle == 0 ? (object)DBNull.Value : article.IdArticle, 
                                        Direction = ParameterDirection.Output ,
                                        Type = SqlDbType.Int
                     },
                    new ParameterSP { Name = "@nombre", Value = article.Name, Type = SqlDbType.VarChar},
                    new ParameterSP { Name = "@pre_unitario", Value = article.Price, Type = SqlDbType.Int},
                    new ParameterSP { Name = "@esta_activo", Value = article.Active, Type = SqlDbType.Bit}
                };

                int rowsAffected = DataHelper.GetInstance().ExecuteSPDML("SP_GUARDAR_ARTICULO", paramList);

                // Recuperar el ID generado
                if (paramList[0].Value != DBNull.Value)
                {
                    article.IdArticle = Convert.ToInt32(paramList[0].Value);
                    return true;
                }

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar el artículo: {ex.Message}", ex);
            }
        }
    }
}
