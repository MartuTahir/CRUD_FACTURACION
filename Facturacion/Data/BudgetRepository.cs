using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Domain;
using Microsoft.Data.SqlClient;

namespace Facturacion.Data
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public BudgetRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public List<Budget> GetAll()
        {
            var budgets = new List<Budget>();

            using (var cmd = new SqlCommand("SP_RECUPERAR_FACTURA", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // mapea factura con su detalle y articulo
                        Budget budget = new Budget()
                        {
                            Id = (int)(reader["id"]),
                            Client = (string)reader["cliente"],
                            Date = (DateTime)(reader["fecha"]),
                            PayMethod = new PayMethod()
                            {
                                Id = (int)(reader["forma_pago"])
                            },
                            Active = (bool)(reader["esta_activa"])
                        };

                        BudgetDetail detail = new BudgetDetail()
                        {
                            Id = (int)(reader["id_detalle"]),
                            Count = (int)(reader["cantidad"]),
                            Article = new Article()
                            {
                                IdArticle = (int)(reader["id_articulo"]),
                                Name = (string)reader["nombre"],
                                Price = (int)reader["pre_unitario"],
                                Active = (bool)(reader["esta_activo"])
                            }
                        };

                        budget.AddDetail(detail); //utiliza el comportamiento definido en clase Budget
                        budgets.Add(budget); //agrega cada factura a la lista que luego retorna el metodo
                    }
                }
            }

            return budgets;
        }

        public Budget GetById(int id)
        {
            Budget budget = null;

            using (var cmd = new SqlCommand("SP_RECUPERAR_FACTURA_POR_ID", _connection, _transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) //por cada factura que lea (deberia ser una) ejecuta el mapeo
                    {
                        if (budget == null)
                        {
                            budget = new Budget()
                            {
                                Id = (int)(reader["id"]),
                                Client = (string)reader["cliente"],
                                Date = (DateTime)(reader["fecha"]),
                                PayMethod = new PayMethod()
                                {
                                    Id = (int)(reader["forma_pago"])
                                },
                                Active = (bool)(reader["esta_activa"])
                            };
                        }

                        var detail = new BudgetDetail()
                        {
                            Id = (int)(reader["id_detalle"]),
                            Count = (int)(reader["cantidad"]),
                            Article = new Article()
                            {
                                IdArticle = (int)(reader["id_articulo"]),
                                Name = (string)reader["nombre"],
                                Price = (int)reader["pre_unitario"],
                                Active = (bool)(reader["esta_activo"])
                            }
                        };

                        budget.AddDetail(detail); //utiliza el comportamiento definido en clase Budget
                    }
                }
            }

            return budget;
        }

        public bool Insert(Budget budget)
        {
            try
            {
                // insertar factura
                using (var cmd = new SqlCommand("SP_INSERTAR_FACTURA", _connection, _transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cliente", budget.Client);
                    cmd.Parameters.AddWithValue("@formaPago", budget.PayMethod.Id);

                    SqlParameter param = new SqlParameter("@id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output //parametro de salida ID
                    };
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();

                    budget.Id = (int)param.Value; // id recuperado del SP
                }
                
                // insertar detalles
                foreach (var detail in budget.Details)
                {
                    using (var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE", _connection, _transaction))
                    {
                        cmdDetail.CommandType = CommandType.StoredProcedure;

                        cmdDetail.Parameters.AddWithValue("@factura", budget.Id);
                        cmdDetail.Parameters.AddWithValue("@articulo", detail.Article.IdArticle);
                        cmdDetail.Parameters.AddWithValue("@cantidad", detail.Count);

                        SqlParameter paramIdDetalle = new SqlParameter("@id_detalle", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmdDetail.Parameters.Add(paramIdDetalle);

                        cmdDetail.ExecuteNonQuery();

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Insert: " + ex.Message);
                return false;
                // rollback lo maneja UnitOfWork
            }
        }

        public bool Update(Budget budget)
        {
            try
            {
                using (var cmd = new SqlCommand("SP_ACTUALIZAR_FACTURA", _connection, _transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", budget.Id);
                    cmd.Parameters.AddWithValue("@cliente", budget.Client);
                    cmd.Parameters.AddWithValue("@formaPago", budget.PayMethod.Id);

                    cmd.ExecuteNonQuery();
                }

                // ya que se maneja desde facturas hay que borrar los detalles y luego reinsertarlos
                using (var cmdDelete = new SqlCommand("DELETE FROM detalles_Factura WHERE id_factura = @id", _connection, _transaction))
                {
                    cmdDelete.Parameters.AddWithValue("@id", budget.Id);
                    cmdDelete.ExecuteNonQuery();
                }

                foreach (var detail in budget.Details)
                {
                    using (var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE", _connection, _transaction))
                    {
                        cmdDetail.CommandType = CommandType.StoredProcedure;
                        cmdDetail.Parameters.AddWithValue("@factura", budget.Id);
                        cmdDetail.Parameters.AddWithValue("@articulo", detail.Article.IdArticle);
                        cmdDetail.Parameters.AddWithValue("@cantidad", detail.Count);

                        SqlParameter paramIdDetalle = new SqlParameter("@id_detalle", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmdDetail.Parameters.Add(paramIdDetalle);

                        cmdDetail.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Update: {ex.Message}");
                return false; // rollback lo maneja UnitOfWork
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var cmd = new SqlCommand("SP_ELIMINAR_FACTURA", _connection, _transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false; // rollback lo maneja UnitOfWork
            }
        }
    }

}
