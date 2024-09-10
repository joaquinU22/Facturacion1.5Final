﻿using Facturacion1._5.Data.Implementations;
using Facturacion1._5.Domain;
using Facturacion1._5.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Facturacion1._5.Data.Utils
{
    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        public bool Add(DetalleFactura detalle)
        {
            var parametros = new List<ParametersSQL>
            {
                new ParametersSQL("@Cantidad", detalle.Cantidad),
                new ParametersSQL("@ArticuloId", detalle.Articulo.Id),
                new ParametersSQL("@IdFactura", detalle.IdFactura),
                new ParametersSQL("@precio", detalle.Precio),
            };
            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("sp_AgregarNuevoDetalleFactura", parametros);
            return filasAfectadas > 0;
        }

        public bool Delete(int id)
        {
            var parametros = new List<ParametersSQL>
            {
                new ParametersSQL("@IdDetalleFactura", id)
            };
            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("sp_EliminarDetalleFactura", parametros);
            return filasAfectadas > 0;
        }

        public List<DetalleFactura> Get()
        {
            var detalles = new List<DetalleFactura>();
            DataTable table = DataHelper.GetInstance().ExecuteSpQuery("sp_TraerDetallesFactura", null);
            if (table != null)
            {
                foreach(DataRow row in table.Rows)
                {
                    var detalle = new DetalleFactura
                    {
                        Id = Convert.ToInt32(row["IdDetalleFactura"]),
                        Cantidad = Convert.ToInt32(row["Cantidad"]),
                        Articulo = new Articulo
                        {
                            Id = Convert.ToInt32(row["ArticuloId"]),
                            Nombre = table.Columns.Contains("Nombre") ? row["Nombre"].ToString() : string.Empty
                        },
                        IdFactura = Convert.ToInt32(row["IdFactura"]),
                        Precio = Convert.ToDouble(row["Precio"])
                        
                    };
                    detalles.Add(detalle);
                }
            }
            return detalles;
        }

        public bool Update(DetalleFactura detalle)
        {
            var parametros = new List<ParametersSQL>
            {
                new ParametersSQL("@IdDetalleFactura", detalle.Id),
                new ParametersSQL("@Cantidad", detalle.Cantidad),
                new ParametersSQL("@ArticuloId", detalle.Articulo.Id),
                new ParametersSQL("@IdFactura", detalle.IdFactura),
                new ParametersSQL("@Precio", detalle.Precio)
            };
            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("sp_ActualizarDetalleFactura", parametros);
            return filasAfectadas > 0;
        }
    }
}
