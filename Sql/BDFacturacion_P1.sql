USE [facturacion_P1]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[id_articulo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NULL,
	[precio_u] [int] NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [pk_articulos] PRIMARY KEY CLUSTERED 
(
	[id_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Detalles_factura]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalles_factura](
	[id_det_factura] [int] NOT NULL,
	[nro_factura] [int] NOT NULL,
	[id_articulo] [int] NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [pk_det_facturas] PRIMARY KEY CLUSTERED 
(
	[nro_factura] ASC,
	[id_det_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[nro_factura] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NULL,
	[id_forma_pago] [int] NULL,
	[cliente] [varchar](50) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [pk_facturas] PRIMARY KEY CLUSTERED 
(
	[nro_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Formas_pago]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Formas_pago](
	[id_forma_pago] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](25) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [pk_formas_pago] PRIMARY KEY CLUSTERED 
(
	[id_forma_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Articulos] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Facturas] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Formas_pago] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Detalles_factura]  WITH CHECK ADD  CONSTRAINT [fk_art_df] FOREIGN KEY([id_articulo])
REFERENCES [dbo].[Articulos] ([id_articulo])
GO
ALTER TABLE [dbo].[Detalles_factura] CHECK CONSTRAINT [fk_art_df]
GO
ALTER TABLE [dbo].[Detalles_factura]  WITH CHECK ADD  CONSTRAINT [fk_facturas_df] FOREIGN KEY([nro_factura])
REFERENCES [dbo].[Facturas] ([nro_factura])
GO
ALTER TABLE [dbo].[Detalles_factura] CHECK CONSTRAINT [fk_facturas_df]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [fk_fp_facturas] FOREIGN KEY([id_forma_pago])
REFERENCES [dbo].[Formas_pago] ([id_forma_pago])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [fk_fp_facturas]
GO
--Inserts
USE [facturacion_P1]
GO

-- INSERT Formas de Pago
INSERT INTO Formas_pago (nombre, activo) VALUES ('Efectivo', 1);
INSERT INTO Formas_pago (nombre, activo) VALUES ('Tarjeta de Crédito', 1);
INSERT INTO Formas_pago (nombre, activo) VALUES ('Transferencia', 1);

-- INSERT Artículos
INSERT INTO Articulos (nombre, precio_u, activo) VALUES ('Martillo', 500, 1);
INSERT INTO Articulos (nombre, precio_u, activo) VALUES ('Taladro', 1500, 1);
INSERT INTO Articulos (nombre, precio_u, activo) VALUES ('Destornillador', 200, 1);
INSERT INTO Articulos (nombre, precio_u, activo) VALUES ('Sierra', 1200, 1);

-- INSERT Facturas
INSERT INTO Facturas (fecha, id_forma_pago, cliente, activo) VALUES ('2025-09-12', 1, 'Juan Perez', 1);
INSERT INTO Facturas (fecha, id_forma_pago, cliente, activo) VALUES ('2025-09-12', 2, 'Maria Gomez', 1);
INSERT INTO Facturas (fecha, id_forma_pago, cliente, activo) VALUES ('2025-09-13', 3, 'Carlos Lopez', 1);

-- INSERT Detalles_factura
-- Para Factura 1
INSERT INTO Detalles_factura (id_det_factura, nro_factura, id_articulo, cantidad) VALUES (1, 1, 1, 2);
INSERT INTO Detalles_factura (id_det_factura, nro_factura, id_articulo, cantidad) VALUES (2, 1, 2, 1);

-- Para Factura 2
INSERT INTO Detalles_factura (id_det_factura, nro_factura, id_articulo, cantidad) VALUES (1, 2, 3, 5);
INSERT INTO Detalles_factura (id_det_factura, nro_factura, id_articulo, cantidad) VALUES (2, 2, 4, 2);

-- Para Factura 3
INSERT INTO Detalles_factura (id_det_factura, nro_factura, id_articulo, cantidad) VALUES (1, 3, 1, 1);
INSERT INTO Detalles_factura (id_det_factura, nro_factura, id_articulo, cantidad) VALUES (2, 3, 3, 3);


/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_ARTICULO]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_ACTUALIZAR_ARTICULO]
	@id_articulo int,
	@nombre varchar(50),
	@precio_u int
AS
BEGIN
    UPDATE Articulos set nombre=@nombre, precio_u=@precio_u where id_articulo=@id_articulo
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_DETALLE_FACTURAS]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_ACTUALIZAR_DETALLE_FACTURAS]
	@id_det_factura int,
    @id_articulo int,
	@nro_factura int,
	@cantidad int
AS
BEGIN
    UPDATE Detalles_factura SET id_articulo =@id_articulo, nro_factura= @nro_factura , cantidad=@cantidad
	WHERE nro_factura = @nro_factura AND id_det_factura = @id_det_factura
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_FACTURA]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_ACTUALIZAR_FACTURA]
	@nro_factura int,
	@fecha date,
	@id_forma_pago int,
	@cliente varchar(50)
AS
BEGIN
    UPDATE Facturas SET fecha =CAST(@fecha AS DATE), id_forma_pago = @id_forma_pago, cliente=@cliente
	where nro_factura=@nro_factura
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_FORMAS_PAGO]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_ACTUALIZAR_FORMAS_PAGO]
	@id int,
    @nombre varchar(50)
AS
BEGIN
    UPDATE Formas_pago SET nombre = @nombre where id_forma_pago = @id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DAR_BAJA_ARTICULO]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DAR_BAJA_ARTICULO]
@id int
AS
BEGIN
	update Articulos set activo = 0 where id_articulo = @id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DAR_BAJA_FACTURA]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_DAR_BAJA_FACTURA]
@id int
AS
BEGIN
	update Facturas set activo = 0 where nro_factura = @id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DAR_BAJA_FORMA_PAGO]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DAR_BAJA_FORMA_PAGO]
@id int
AS
BEGIN
	update Formas_pago set activo = 0 where id_forma_pago = @id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDAR_ARTICULO]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GUARDAR_ARTICULO]
@nombre varchar(50),
@precio_u int
AS
BEGIN
	insert into Articulos(nombre, precio_u) values(@nombre, @precio_u)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDAR_DETALLE_FACTURAS]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GUARDAR_DETALLE_FACTURAS]
	@id_det_factura int,
    @id_articulo int,
	@nro_factura int,
	@cantidad int
AS
BEGIN
    insert into Detalles_factura(id_det_factura, id_articulo, nro_factura, cantidad) values(@id_det_factura, @id_articulo, @nro_factura, @cantidad)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDAR_FACTURA]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GUARDAR_FACTURA]
    @fecha date,
    @id_forma_pago int,
    @cliente varchar(50),
	@nro_factura int OUTPUT
AS
BEGIN
    INSERT INTO Facturas (fecha, id_forma_pago, cliente)
    VALUES (@fecha, @id_forma_pago, @cliente)
	SET @nro_factura=SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDAR_FORMA_PAGO]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_GUARDAR_FORMA_PAGO]
    @nombre varchar(50)
AS
BEGIN
    INSERT INTO formas_pago(nombre) values(@nombre)
end
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_ARTICULOS]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_TRAER_ARTICULOS]
AS
BEGIN
	SELECT *
	FROM Articulos
	WHERE activo = 1
	order by 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_ARTICULOS_POR_ID]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_TRAER_ARTICULOS_POR_ID]
@id int
AS
BEGIN
	SELECT id_articulo, nombre, precio_u
	FROM Articulos
	WHERE @id=id_articulo and activo = 1
	order by 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_DETALLES_FACTURA]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_TRAER_DETALLES_FACTURA]
@id INT
AS
BEGIN
    SELECT 
        df.id_det_factura,
        df.nro_factura,
		a.id_articulo,
        a.nombre AS articulo,
        df.cantidad,
        a.precio_u,
        (df.cantidad * a.precio_u) AS subtotal
    FROM Detalles_factura df
    JOIN Articulos a ON df.id_articulo = a.id_articulo
    WHERE df.nro_factura = @id
    ORDER BY df.id_det_factura
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_FACTURAS]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_TRAER_FACTURAS]
AS
BEGIN
	SELECT distinct f.nro_factura, f.fecha, fp.id_forma_pago, fp.nombre, f.cliente
	FROM Facturas f join 
	Formas_pago as fp on f.id_forma_pago = fp.id_forma_pago
	WHERE f.activo = 1
	Order by 1,2
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_FACTURAS_POR_ID]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_TRAER_FACTURAS_POR_ID]
@id int
AS
BEGIN
	SELECT f.nro_factura, f.fecha, fp.id_forma_pago, fp.nombre, f.cliente, FP.nombre	
	FROM Facturas f join 	
	Formas_pago fp on f.id_forma_pago=fp.id_forma_pago
	WHERE @id= f.nro_factura and f.activo = 1
	Order by 1,2
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_FORMA_PAGO_POR_ID]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_TRAER_FORMA_PAGO_POR_ID]
@id int
AS
BEGIN
	SELECT id_forma_pago, nombre
	FROM Formas_pago
	where @id=id_forma_pago and activo = 1
	ORDER BY id_forma_pago
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_FORMAS_PAGO]    Script Date: 12/9/2025 22:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_TRAER_FORMAS_PAGO]
AS
BEGIN
	SELECT id_forma_pago, nombre
	FROM Formas_pago
	WHERE activo = 1
	ORDER BY id_forma_pago
END
GO

--Borrar registros y reiniciar Identity
--DBCC CHECKIDENT('Detalles_factura', RESEED, 0)
--DELETE FROM Detalles_factura

--DBCC CHECKIDENT ('Articulos', RESEED, 0);
--DELETE FROM Articulos

--DBCC CHECKIDENT ('Formas_pago', RESEED, 0);
--DELETE FROM Formas_pago

--DBCC CHECKIDENT ('Facturas', RESEED, 0);
--Delete FROM Facturas