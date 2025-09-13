USE [db_libreriaPROG]
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[forma_pago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_forma_pago] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[detalles_Factura](
	[id_detalle] [int] IDENTITY(1,1) NOT NULL,
	[id_factura] [int] NOT NULL,
	[id_articulo] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
 CONSTRAINT [PK_Detalles_Factura] PRIMARY KEY CLUSTERED 
(
	[id_detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[facturas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cliente] [varchar](50) NOT NULL,
	[forma_pago] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[esta_activa] [bit] NOT NULL,
 CONSTRAINT [PK_facturas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/******  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[articulos](
	[id_articulo] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[pre_unitario] [int] NOT NULL,
	[esta_activo] [bit] NOT NULL,
 CONSTRAINT [PK_articulos] PRIMARY KEY CLUSTERED 
(
	[id_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[forma_pago] ([nombre]) VALUES ('Debito')
GO
INSERT [dbo].[forma_pago] ([nombre]) VALUES ('Credito')
GO
INSERT [dbo].[forma_pago] ([nombre]) VALUES ('Efectivo')
GO
INSERT [dbo].[detalles_Factura] ( [id_factura], [id_articulo], [cantidad]) VALUES ( 1, 1, 10)
GO
INSERT [dbo].[detalles_Factura] ( [id_factura], [id_articulo], [cantidad]) VALUES ( 2, 1, 1)
GO
INSERT [dbo].[detalles_Factura] ( [id_factura], [id_articulo], [cantidad]) VALUES ( 1, 2, 5)
GO
SET IDENTITY_INSERT [dbo].[facturas] ON 
GO
INSERT [dbo].[facturas] ([id], [cliente], [forma_pago], [fecha], [esta_activa]) VALUES (1, N'Cliente de prueba', 1, CAST(N'2024-09-02T16:58:03.200' AS DateTime), 1)
GO
INSERT [dbo].[facturas] ([id], [cliente], [forma_pago], [fecha], [esta_activa]) VALUES (2, N'Cliente 2', 2, CAST(N'2024-09-02T17:08:18.470' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[facturas] OFF
GO
SET IDENTITY_INSERT [dbo].[articulos] ON 
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (1, N'TEST', 1000, 0)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (2, N'PRODUCTO DE PRUEBA 2', 2000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (3, N'PRODUCTO DE PRUEBA 3', 2000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (4, N'PRODUCTO DE PRUEBA 4', 2000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (5, N'PRODUCTO DE PRUEBA 5', 2000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (6, N'PRODUCTO DE PRUEBA 6', 2000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (7, N'PRODUCTO DE PRUEBA 7', 2000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (8, N'PRODUCTO DE PRUEBA 8', 2000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (9, N'PRODUCTO DE PRUEBA 9', 3000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (10, N'PRODUCTO DE PRUEBA 10', 4000, 1)
GO
INSERT [dbo].[articulos] ([id_articulo], [nombre], [pre_unitario], [esta_activo]) VALUES (11, N'PRODUCTO DE PRUEBA 11', 3000, 1)
GO
SET IDENTITY_INSERT [dbo].[articulos] OFF
GO
ALTER TABLE [dbo].[detalles_Factura]  WITH CHECK ADD  CONSTRAINT [FK_Detalles_Facturas_Facturas] FOREIGN KEY([id_factura])
REFERENCES [dbo].[facturas] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[detalles_Factura] CHECK CONSTRAINT [FK_Detalles_Facturas_Facturas]
GO
ALTER TABLE [dbo].[detalles_Factura]  WITH CHECK ADD  CONSTRAINT [FK_Detalles_Facturas_Articulos] FOREIGN KEY([id_articulo])
REFERENCES [dbo].[articulos] ([id_articulo])
GO
ALTER TABLE [dbo].[detalles_Factura] CHECK CONSTRAINT [FK_Detalles_Facturas_Articulos]
GO
ALTER TABLE [dbo].[facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Forma_Pago] FOREIGN KEY([forma_pago])
REFERENCES [dbo].[forma_pago] ([id])
GO
ALTER TABLE [dbo].[facturas] CHECK CONSTRAINT [FK_Facturas_Forma_Pago]
GO
/******  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDAR_ARTICULO]
@id_articulo int OUTPUT,
@nombre varchar(20),
@pre_unitario int
AS
BEGIN 
	IF @id_articulo = 0
		BEGIN
			insert into articulos(nombre, pre_unitario, esta_activo) 
			values (@nombre,@pre_unitario, 1)
			SET @id_articulo = SCOPE_IDENTITY();
		END
	ELSE
		BEGIN
			update articulos 
			set nombre= @nombre, pre_unitario= @pre_unitario 
			where id_articulo=@id_articulo
		END
END
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLE] 
	@factura int,
	@articulo int,
	@cantidad int

AS
BEGIN
	INSERT INTO detalles_Factura(id_factura, id_articulo, cantidad) VALUES ( @factura, @articulo, @cantidad);
	
END
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_FACTURA] 
	@cliente varchar(50),
	@id int output,
	@formaPago int
AS
BEGIN
	INSERT INTO facturas(cliente, fecha, forma_pago, esta_activa) VALUES (@cliente, GETDATE(), @formaPago, 1);
	SET @id = SCOPE_IDENTITY();
END
GO
/******  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_RECUPERAR_FACTURA_POR_ID]
	@id int
AS
BEGIN
	SELECT f.*, df.cantidad, a.*
	  FROM facturas f
	  INNER JOIN detalles_Factura df ON df.id_factura =f.id
	  INNER JOIN articulos a ON (a.id_articulo = df.id_articulo)
	  WHERE f.id = @id;
END
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_RECUPERAR_FACTURA]
AS
BEGIN
	SELECT f.*, df.cantidad, a.*
	  FROM facturas f
	  INNER JOIN detalles_Factura df ON df.id_factura =f.id
	  INNER JOIN articulos a ON (a.id_articulo = df.id_articulo)

	  ORDER BY f.id;
END
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_RECUPERAR_ARTICULO_POR_CODIGO]
	@id_articulo int
AS
BEGIN
	SELECT * FROM articulos WHERE id_articulo = @id_articulo;
END
GO
/******  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_ARTICULOS] 
AS
BEGIN
	SELECT * FROM articulos
END
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/******  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RECUPERAR_FORMAS_PAGO] 
AS
BEGIN
	SELECT * FROM forma_pago
END
GO
/****** ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_REGISTRAR_BAJA_ARTICULO] 
	@id_articulo int 

AS
BEGIN
	UPDATE articulos SET esta_activo = 0 WHERE id_articulo = @id_articulo;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SP_ACTUALIZAR_FACTURA
    @id INT,
    @cliente VARCHAR(50),
    @formaPago INT
AS
BEGIN
    UPDATE facturas
    SET cliente = @cliente,
        forma_pago = @formaPago
    WHERE id = @id;
END
GO

CREATE PROCEDURE SP_ELIMINAR_FACTURA
    @id INT
AS
BEGIN
    DELETE FROM facturas WHERE id = @id;
END
GO

DECLARE @id_articulo INT = 0;

EXEC SP_GUARDAR_ARTICULO @nombre = 'Lapiz BIC', @precio = 2000, @activo = 1, @id_articulo = @id_articulo OUTPUT;

SELECT @id_articulo AS IdGenerado;
