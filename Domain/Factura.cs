using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Domain
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdFormaPago { get; set; }
        public int ClienteId { get; set; }
        public List<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
        public double Total()
        {
            double total = 0;
            foreach (DetalleFactura detail in DetalleFacturas)
            {
                total += detail.SubTotal();
            }
            return total;
        }
    }
}
