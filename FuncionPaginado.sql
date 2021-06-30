USE [practicas]
GO

/****** Object:  UserDefinedFunction [dbo].[fnPaginar]    Script Date: 30/06/2021 03:17:38 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fnPaginar]
(
	@pagina int,
	@cantidadReg int
)
RETURNS 
@lst TABLE
(
	Id int,
	Producto varchar(MAX),
	Descripcion varchar(MAX),
	Codigo varchar(MAX),
	Cantidad int,
	Precio int
	
)
AS
BEGIN
	declare @regIgnorados int = @pagina * @cantidadReg
	insert into @lst (Id,Producto,Descripcion,Codigo,Cantidad,Precio)
	select * from Inventario 
	order by Id
	offset @regIgnorados rows
	fetch next @cantidadReg rows only
	
	RETURN 
END
GO

