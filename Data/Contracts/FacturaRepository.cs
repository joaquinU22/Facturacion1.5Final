using Facturacion1._5.Data.Implementations;
using Facturacion1._5.Domain;
using Facturacion1._5.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Facturacion1._5.Data.Utils
{
    public class FacturaRepository : IFacturaRepository
    {
        public bool Add(Factura factura)
        {
            bool result = true;
            SqlConnection? connection = null;
            SqlTransaction? transaction = null;    
            try
            {
                connection = DataHelper.GetInstance().GetConnection();
                connection.Open();
                transaction = connection.BeginTransaction();    

                var cmd = new SqlCommand("sp_AgregarNuevaFactura", connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                //parámetro de entrada:
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@IdFormaPago", factura.IdFormaPago);
                cmd.Parameters.AddWithValue("@IdCliente", factura.ClienteId);

                SqlParameter param = new SqlParameter("@IdFactura", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();


               
                //verificar si hay detalles de factura
                int idFacturas = (int)param.Value;
                if (factura.DetalleFacturas.Count == 0)
                {
                    transaction.Rollback();
                    
                }
                foreach (var detalle in factura.DetalleFacturas)
                {
                    var cmdDetalle = new SqlCommand("sp_AgregarNuevoDetalleFactura", connection, transaction); 
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@ArticuloId", detalle.Articulo.Id);
                    cmdDetalle.Parameters.AddWithValue("@IdFactura", idFacturas);
                    cmdDetalle.Parameters.AddWithValue("@Precio", detalle.Precio);
                    cmdDetalle.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine($"Error: {ex.Message}");
                result = false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        public bool Delete(int Id)
        {
            bool result = true;
            SqlConnection? connection = null;
            SqlTransaction? transaction = null;
            try
            {
                connection = DataHelper.GetInstance().GetConnection();
                connection.Open();
                transaction = connection.BeginTransaction();
                //llamamos al sp para eliminar la factura
                var cmd = new SqlCommand("sp_EliminarFactura", connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdFactura", Id);
                cmd.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (SqlException)
            {
                if (transaction != null) transaction.Rollback();
                result = false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        public List<Factura> GetAll()
        {
            var facturas = new List<Factura>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSpQuery("sp_TraerFacturas", null);

            foreach (DataRow row in t.Rows)
            {
                int id = Convert.ToInt32(row["IdFactura"]);
                DateTime fecha = Convert.ToDateTime(row["Fecha"]);
                int idFormaPago = Convert.ToInt32(row["IdFormaPago"]);
                int idCliente = Convert.ToInt32(row["IdCliente"]);
                Factura factura = new Factura
                {
                    Id = id,
                    Fecha = fecha,
                    IdFormaPago = idFormaPago,
                    ClienteId = idCliente,
                };
                facturas.Add(factura);
            }
            return facturas;
        }
    }
}
