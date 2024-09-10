using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Facturacion1._5.Utils
{
    public class DataHelper
    {
        private static DataHelper _istancia;
        private SqlConnection _connection;
        private DataHelper()
        {
            string connectionString = "Data Source=DESKTOP-C1J1PRA\\MSSQL2022;Initial Catalog=Facturacion152;Integrated Security=True";

            _connection = new SqlConnection(connectionString);
        }
        public static DataHelper GetInstance()
        {
            if (_istancia == null)
                _istancia = new DataHelper();
            return _istancia;
        }
        public SqlConnection GetConnection()
        {
            return _connection;
        }
        public DataTable ExecuteSpQuery(string sp, List<ParametersSQL>? parameters)
        {
            DataTable t = new DataTable();
            SqlCommand cmd = null;

            try
            {
                _connection.Open();
                cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                t.Load(cmd.ExecuteReader());
            }
            catch (SqlException)
            {
                t = null;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return t;
        }
        public int ExecuteSPDML(string sp, List<ParametersSQL>? parameters)
        {
            int rows;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return rows;
        }
    }
}
