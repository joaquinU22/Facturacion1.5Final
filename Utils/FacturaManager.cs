using Facturacion1._5.Data.Implementations;
using Facturacion1._5.Data.Utils;
using Facturacion1._5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion1._5.Utils
{
    public class FacturaManager
    {
        private IFacturaRepository facturaRepository;
        private IArticuloRepository articuloRepository;
        private IClienteRepository clienteRepository;
        private IDetalleFacturaRepository detalleFacturarepository;
        private IFormaPagoRepository formaPagoRepository;
        public FacturaManager()
        {
            facturaRepository = new FacturaRepository();
            articuloRepository = new ArticuloRepository();
            clienteRepository = new ClienteRepository();
            detalleFacturarepository = new DetalleFacturaRepository();
            formaPagoRepository = new FormaPagoRepository();
        }

        public List<Factura> GetAllFacturas()
        {
            return facturaRepository.GetAll();
        }

        public bool Add(Factura factura)
        {
            return facturaRepository.Add(factura);
        }
        public bool Delete(int Id)
        {
            return facturaRepository.Delete(Id);
        }
        public bool AddArticulo(Articulo articulo)
        {
            return articuloRepository.Add(articulo);
        }
        public bool DeleteArticulo(int id)
        {
            return articuloRepository.Delete(id);
        }
        public List<Articulo> GetAllArticulo() 
        { 
            return articuloRepository.GetAll();
        }

        public bool UpdateArticulo(Articulo articulo)
        {
            return articuloRepository.Update(articulo);
        }
        public List<Cliente> GetAllCliente()
        {
            return clienteRepository.GetAll();
        }
        public bool AddCliente(Cliente cliente)
        {
            return clienteRepository.Add(cliente);
        }
        public bool AddDetalle(DetalleFactura detalle)
        {
            return detalleFacturarepository.Add(detalle);
        }
        public List<DetalleFactura> Get()
        {
            return detalleFacturarepository.Get();
        }
        public bool UpdateDetalle(DetalleFactura detalle)
        {
            return detalleFacturarepository.Update(detalle) ;
        }
        public bool DeleteFactura(int id)
        {
            return detalleFacturarepository.Delete(id);
        }
        public List<FormaPago> GetAllFormaPago()
        {
            return formaPagoRepository.GetAll();
        }
    }
}
