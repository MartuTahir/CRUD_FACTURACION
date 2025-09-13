using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Facturacion.Data.Utils
{
    public class DataHelper
    {
        //implementacion del patron de diseño singleton para conectar a la bd
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.CadenaConexionLocal);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp, List<ParameterSP>? param = null)
        {
            DataTable dt = new DataTable();

            try
            {
                //conexion 
                _connection.Open();

                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

                //agregar parametros si los hay
                if (param != null)
                {
                    foreach (ParameterSP p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }

                dt.Load(cmd.ExecuteReader());

            }
            catch (SqlException ex)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }

            return dt;
        }

        public int ExecuteSPDML(string sp, List<ParameterSP>? param = null) //ejecutar sp para dml
        {
            int rowsAffected = 0;

            try
            {
                _connection.Open();

                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // agregar parámetros si los hay
                if (param != null)
                {
                    foreach (ParameterSP p in param)
                    {
                        var sqlParam = new SqlParameter(p.Name, p.Value ?? DBNull.Value)
                        {
                            Direction = p.Direction,                      
                        };

                        // asignar size si el tipo lo requiere | preguntar al profe
                        if (p.Type == SqlDbType.VarChar || p.Type == SqlDbType.NVarChar || p.Type == SqlDbType.Char)
                        {
                            sqlParam.SqlDbType = p.Type;
                            sqlParam.Size = p.Size > 0 ? p.Size : 30;
                        }
                        else
                        {
                            sqlParam.SqlDbType = p.Type;
                        }

                        cmd.Parameters.Add(sqlParam);
                    }

                }

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected <= 0)
                    throw new Exception("El SP no afectó ninguna fila. Verificá los parámetros o el estado de la base de datos.");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ExecuteSPDML: {ex.Message}", ex);
            }
            finally
            {
                _connection.Close();
            }

            return rowsAffected;
        }

        public SqlConnection GetConnection() 
        {
            return _connection;
        }

    }
}
