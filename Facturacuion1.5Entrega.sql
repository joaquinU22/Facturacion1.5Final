CREATE DATABASE Facturacion152;
GO

USE Facturacion152;
GO

-- Crear tabla Articulo
CREATE TABLE Articulo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    PrecioUnitario DECIMAL(18, 2) NOT NULL
);
GO

-- Crear tabla Cliente
CREATE TABLE Cliente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Dni NVARCHAR(20) NOT NULL
);
GO

-- Crear tabla FormaPago
CREATE TABLE FormaPago (
    IdFormaPago INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- Crear tabla Factura
CREATE TABLE Factura (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME NOT NULL,
    IdFormaPago INT NOT NULL,
    IdCliente INT NOT NULL,
    FOREIGN KEY (IdFormaPago) REFERENCES FormaPago(IdFormaPago),
    FOREIGN KEY (IdCliente) REFERENCES Cliente(Id)
);
GO

-- Crear tabla DetalleFactura
CREATE TABLE DetalleFactura (
    IdDetalleFactura INT IDENTITY(1,1) PRIMARY KEY,
    Cantidad INT NOT NULL,
    ArticuloId INT NOT NULL,
    IdFactura INT NOT NULL,
    Precio DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ArticuloId) REFERENCES Articulo(Id),
    FOREIGN KEY (IdFactura) REFERENCES Factura(IdFactura)
);
GO

-- Procedimiento para agregar una nueva factura
CREATE PROCEDURE sp_AgregarNuevaFactura
    @Fecha DATE,
    @IdFormaPago INT,
    @IdCliente INT,
    @IdFactura INT OUTPUT
AS
BEGIN

    -- Asignar un nuevo IdFactura
    SET NOCOUNT ON;

    INSERT INTO Factura (Fecha, IdFormaPago, IdCliente)
    VALUES (@Fecha, @IdFormaPago, @IdCliente);

    -- Obtener el IdFactura del nuevo registro
    SET @IdFactura = SCOPE_IDENTITY();
END


-- Procedimiento para agregar un detalle de factura
CREATE PROCEDURE sp_AgregarNuevoDetalleFactura
    @Cantidad INT,
    @ArticuloId INT,
    @IdFactura INT,
    @Precio DECIMAL(18, 2)
AS
BEGIN
    -- Verificar si la factura existe y realizar la inserción si es así
    IF EXISTS (SELECT 1 FROM Factura WHERE IdFactura = @IdFactura)
    BEGIN
        INSERT INTO DetalleFactura (Cantidad, ArticuloId, IdFactura, Precio)
        VALUES (@Cantidad, @ArticuloId, @IdFactura, @Precio);
    END
    -- Si la factura no existe, no hacemos nada
    -- No se lanza un error ni se imprime un mensaje
END;
GO




-- Procedimiento para actualizar un artículo
CREATE PROCEDURE sp_ActualizarArticulo
    @Id INT, 
    @Nombre NVARCHAR(100),
    @PrecioUnitario DECIMAL(18,2)
AS
BEGIN
    UPDATE Articulo
    SET Nombre = @Nombre,
        PrecioUnitario = @PrecioUnitario
    WHERE Id = @Id;
END;
GO

-- Procedimiento para actualizar un detalle de factura
CREATE PROCEDURE sp_ActualizarDetalleFactura
    @IdDetalleFactura INT,
    @Cantidad INT,
    @ArticuloId INT,
    @IdFactura INT,
    @Precio DECIMAL(18, 2)
AS
BEGIN
    UPDATE DetalleFactura
    SET Cantidad = @Cantidad,
        ArticuloId = @ArticuloId,
        IdFactura = @IdFactura,
        Precio = @Precio
    WHERE IdDetalleFactura = @IdDetalleFactura;
END;
GO

-- Procedimiento para actualizar una factura
CREATE PROCEDURE sp_ActualizarFactura
    @IdFactura INT,
    @Fecha DATETIME,
    @IdFormaPago INT,
    @IdCliente INT
AS
BEGIN
    UPDATE Factura
    SET Fecha = @Fecha,
        IdFormaPago = @IdFormaPago,
        IdCliente = @IdCliente
    WHERE IdFactura = @IdFactura;
END;
GO

-- Procedimiento para agregar un cliente
CREATE PROCEDURE sp_AgregarCliente
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Dni NVARCHAR(20)
AS
BEGIN
    INSERT INTO Cliente (Nombre, Apellido, Dni)
    VALUES (@Nombre, @Apellido, @Dni);
END;
GO

-- Procedimiento para eliminar un artículo
CREATE PROCEDURE sp_EliminarArticulo
    @Id INT
AS
BEGIN
    DELETE FROM Articulo
    WHERE Id = @Id;
END;
GO

-- Procedimiento para eliminar un detalle de factura
CREATE PROCEDURE sp_EliminarDetalleFactura
    @IdDetalleFactura INT
AS
BEGIN
    DELETE FROM DetalleFactura
    WHERE IdDetalleFactura = @IdDetalleFactura;
END;
GO

-- Procedimiento para eliminar una factura
CREATE PROCEDURE sp_EliminarFactura
    @IdFactura INT
AS
BEGIN
    DELETE FROM Factura
    WHERE IdFactura = @IdFactura;
END;
GO

-- Procedimiento para traer todos los artículos
CREATE PROCEDURE sp_TraerArticulos
AS
BEGIN
    SELECT * FROM Articulo;
END;
GO

-- Procedimiento para traer todos los clientes
CREATE PROCEDURE sp_TraerCliente
AS
BEGIN
    SELECT * FROM Cliente;
END;
GO

-- Procedimiento para traer todos los detalles de factura
CREATE PROCEDURE sp_TraerDetallesFactura
AS
BEGIN
    SELECT * FROM DetalleFactura;
END;
GO

-- Procedimiento para traer todas las facturas
CREATE PROCEDURE sp_TraerFacturas
AS
BEGIN
    SELECT * FROM Factura;
END;
GO

-- Procedimiento para traer todas las formas de pago
CREATE PROCEDURE sp_TraerFormasPago
AS
BEGIN
    SELECT * FROM FormaPago;
END;
GO

-- Procedimiento para agregar un artículo
CREATE PROCEDURE sp_AgregarArticulo
    @Nombre NVARCHAR(100),
    @PrecioUnitario DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Articulo (Nombre, PrecioUnitario)
    VALUES (@Nombre, @PrecioUnitario);
END;
GO

-- Agregar Formas de Pago
INSERT INTO FormaPago (Nombre) VALUES ('Efectivo');
INSERT INTO FormaPago (Nombre) VALUES ('Tarjeta de Crédito');
INSERT INTO FormaPago (Nombre) VALUES ('Transferencia Bancaria');
GO

-- Agregar Clientes
INSERT INTO Cliente (Nombre, Apellido, Dni) VALUES ('Pepa', 'Pérez', '4388765');
INSERT INTO Cliente (Nombre, Apellido, Dni) VALUES ('Ana', 'Paula', '12456786');
GO
-- Agregar Artículos
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Paracetamol', 100.00);
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Ibuprofeno', 150.00);
INSERT INTO Articulo (Nombre, PrecioUnitario) VALUES ('Vitamina C', 200.00);
GO

-- Agregar Facturas
DECLARE @IdFactura INT;

EXEC sp_AgregarNuevaFactura @Fecha = '2024-09-01', @IdFormaPago = 1, @IdCliente = 1, @IdFactura = @IdFactura OUTPUT;
-- Agregar detalles a la primera factura
EXEC sp_AgregarNuevoDetalleFactura @Cantidad = 2, @ArticuloId = 1, @IdFactura = @IdFactura, @Precio = 100.00;

EXEC sp_AgregarNuevaFactura @Fecha = '2024-09-02', @IdFormaPago = 2, @IdCliente = 2, @IdFactura = @IdFactura OUTPUT;
-- Agregar detalles a la segunda factura
EXEC sp_AgregarNuevoDetalleFactura @Cantidad = 1, @ArticuloId = 2, @IdFactura = @IdFactura, @Precio = 150.00;
EXEC sp_AgregarNuevoDetalleFactura @Cantidad = 3, @ArticuloId = 3, @IdFactura = @IdFactura, @Precio = 200.00;
GO

dro

