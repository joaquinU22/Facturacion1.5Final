using Facturacion1._5.Data.Utils;
using Facturacion1._5.Domain;
using System.Data.SqlClient;
using System.Data;
using Facturacion1._5.Utils;


FacturaManager serviceManager = new FacturaManager();

// Mostrar todas las facturas disponibles
Console.WriteLine("Facturas Disponibles:");
var facturas = serviceManager.GetAllFacturas();
foreach (var factura in facturas)
{
    Console.WriteLine($"ID: {factura.Id}, Fecha: {factura.Fecha}");
}

// Agregar una nueva factura
var nuevaFactura = new Factura
{
    Fecha = DateTime.Now,
    IdFormaPago = 1,
    ClienteId = 1,
    DetalleFacturas = new List<DetalleFactura>
    {
         new DetalleFactura
         {
             Cantidad = 1,
             Articulo = new Articulo { Id = 1 },
             Precio = 100.00
         }
    }
};

bool seGuardoFactura = serviceManager.Add(nuevaFactura);
if (seGuardoFactura)
{
    Console.WriteLine("Factura guardado correctamente.");
}
else
{
    Console.WriteLine("Error al guardar factura.");
}


// Agregar un nuevo artículo
var nuevoArticulo = new Articulo
{
    Nombre = "Pan",
    PrecioUnitario = 50.00
};

bool seGuardoArticulo = serviceManager.AddArticulo(nuevoArticulo);
if (seGuardoArticulo)
{
    Console.WriteLine("Articulo guardado correctamente.");
}
else
{
    Console.WriteLine("Error al guardar Articulo.");
}


// Mostrar todos los artículos
Console.WriteLine("\nArtículos Disponibles:");
var articulos = serviceManager.GetAllArticulo();
foreach (var articulo in articulos)
{
    Console.WriteLine($"ID: {articulo.Id}, Nombre: {articulo.Nombre}, Precio: {articulo.PrecioUnitario}");
}

// Actualizar un artículo
if (articulos.Count > 0)
{
    var articuloAActualizar = articulos[0];
    articuloAActualizar.PrecioUnitario = 60.00;
    bool seActualizoArticulo = serviceManager.UpdateArticulo(articuloAActualizar);
    if (seActualizoArticulo)
    {
        Console.WriteLine("Articulo actualizado correctamente.");
    }
    else
    {
        Console.WriteLine("Error al actualizar articulo.");
    }
}

// Agregar un nuevo cliente
var nuevoCliente = new Cliente
{
    Nombre = "Sergio",
    Apellido = "Alvarez",
    Dni = "112345"
};

bool seGuardoCliente = serviceManager.AddCliente(nuevoCliente);
if (seGuardoCliente)
{
    Console.WriteLine("Cliente fuardado correctamente.");
}
else
{
    Console.WriteLine("Error al guardar cliente.");
}
// Mostrar todos los clientes
Console.WriteLine("\nClientes Disponibles:");
var clientes = serviceManager.GetAllCliente();
foreach (var cliente in clientes)
{
    Console.WriteLine($"ID: {cliente.Id}, Nombre: {cliente.Nombre}");
}

// Agregar un detalle de factura
var nuevoDetalle = new DetalleFactura
{
    Cantidad = 2,
    Articulo = new Articulo { Id = 1 },
    Precio = 100.00
};

bool seGuardoDetalle = serviceManager.AddDetalle(nuevoDetalle);
if (seGuardoDetalle)
{
    Console.WriteLine("Detalle de factura guardado correctamente.");
}
else
{
    Console.WriteLine("Error al guardar el detalle de factura. Verifica que todos los datos sean correctos.");
}


// Mostrar todos los detalles de factura
Console.WriteLine("\nDetalles de Factura Disponibles:");
var detalles = serviceManager.Get();
foreach (var detalle in detalles)
{
    Console.WriteLine($"ID: {detalle.Id}, Cantidad: {detalle.Cantidad}, Artículo ID: {detalle.Articulo.Id}, Precio: {detalle.Precio}");
}

// Actualizar un detalle de factura
if (detalles.Count > 0)
{
    var detalleAActualizar = detalles[0];
    detalleAActualizar.Precio = 120.00;
    bool seActualizoDetalle = serviceManager.UpdateDetalle(detalleAActualizar);
    if (seActualizoDetalle)
    {
        Console.WriteLine("Detalle facturas actualizado correctamente.");
    }
    else
    {
        Console.WriteLine("Error al actualizar detalle factura.");
    }
}

// Eliminar una factura (demostración)
if (facturas.Count > 0)
{
    var idFacturaAEliminar = facturas[0].Id;
    bool seEliminoFactura = serviceManager.Delete(idFacturaAEliminar);
    if (seEliminoFactura)
    {
        Console.WriteLine("Factura eliminado correctamente.");
    }
    else
    {
        Console.WriteLine("Error al eliminar Factura.");
    }
}

// Mostrar todas las formas de pago
Console.WriteLine("\nFormas de Pago Disponibles:");
var formasPago = serviceManager.GetAllFormaPago();
foreach (var formaPago in formasPago)
{
    Console.WriteLine($"ID: {formaPago.Id}, Descripción: {formaPago.Nombre}");
}

Console.WriteLine("\nPruebas completadas. Presiona cualquier tecla para salir.");

    
