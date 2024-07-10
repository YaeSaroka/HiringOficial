-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE CargaPerfilDefault
	-- Add the parameters for the stored procedure here
	@id int, @estilo varchar(10), @foto_perfil varchar(max), @encabezado varchar(max), @nombre_apellido varchar(max), @telefono varchar(max), @mail varchar(320)
AS
BEGIN
	INSERT INTO Informacion_Personal_Empleado(id,estilo,foto_perfil,encabezado, nombre_apellido, telefono, mail)
	VALUES (@id,   @estilo,   @foto_perfil,   @encabezado,    @nombre_apellido,    @telefono,    @mail)
	 
END
GO
