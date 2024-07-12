USE [Hiring]
GO
/****** Object:  StoredProcedure [dbo].[InsertarInformacionPersonalEmpleado1]    Script Date: 10/7/2024 10:24:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[InsertarInformacionPersonalEmpleado1]
	@id int, @Nombre_Apellido varchar(max), @Mail varchar(254),@telefono varchar(max),  @estilo varchar(max)
	AS
BEGIN
	UPDATE Informacion_Personal_Empleado
	SET nombre_apellido = @Nombre_Apellido , mail= @Mail ,telefono= @telefono, estilo= @estilo 
	WHERE id = @id

END
