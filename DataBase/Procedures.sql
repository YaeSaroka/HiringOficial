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
<<<<<<< HEAD
CREATE PROCEDURE [dbo].[LoginUsuario]
	-- Add the parameters for the stored procedure here
	@pMail varchar(150), @pContraseña varchar(150)
AS
BEGIN
	SELECT * FROM Usuario WHERE @pMail=Mail AND @pContraseña=Contraseña
END
GO

CREATE PROCEDURE [dbo].[Login_VerificarContraseña]
	-- Add the parameters for the stored procedure here
	@Contraseña varchar(150)
AS
BEGIN
	SELECT Contraseña FROM Usuario WHERE  @Contraseña=Contraseña 
END
GO


CREATE PROCEDURE [dbo].[Registro_VerificarExistencia]
	-- Add the parameters for the stored procedure here
	@Mail varchar
AS
BEGIN
	SELECT Mail FROM Usuario WHERE @Mail= Mail
END
GO

CREATE PROCEDURE [dbo].[Registro]
	-- Add the parameters for the stored procedure here
	@Mail varchar(150), @Contraseña varchar(150)
AS
BEGIN
	INSERT INTO Usuario(Mail, Contraseña) VALUES (@Mail, @Contraseña)
END
GO

CREATE PROCEDURE [dbo].[OlvideMiContraseña]
	-- Add the parameters for the stored procedure here
	@Mail varchar(150)
AS
BEGIN
	SELECT Contraseña FROM Usuario WHERE @Mail=Mail
END
GO




/*----------------------------------------------------------------------------------------------------------------*/


CREATE PROCEDURE InsertarInformacionPersonalEmpleado1
	@id int, @Nombre_Apellido varchar(max), @Mail varchar(254),@telefono varchar(max),  @estilo varchar(max)
	AS
BEGIN
	INSERT INTO Informacion_Personal_Empleado(id, nombre_apellido, mail, telefono,estilo)
	VALUES (@id,@Nombre_Apellido,@Mail,@telefono,@estilo)
END
GO

CREATE PROCEDURE InsertarInformacionPersonalEmpleado2
	 @acerca_de_mi varchar(max), @profesion_actual varchar(max), @ubicacion varchar(max), @foto_perfil varchar(max)
AS
BEGIN
	INSERT INTO Informacion_Personal_Empleado(acerca_de_mi, profesion_actual,ubicacion, foto_perfil)
	VALUES (@acerca_de_mi, @profesion_actual,@ubicacion,@foto_perfil )
END
GO


CREATE PROCEDURE InsertarNecesidad
	@Nombre varchar(max), @id_info_empleado int
AS
BEGIN
	INSERT INTO Necesidad
	VALUES (@Nombre, @id_info_empleado)
END
GO

CREATE PROCEDURE SelectNecesidad
	@id_info_empleado int
AS
BEGIN
	SELECT nombre
	FROM Necesidad
	WHERE id_info_empleado=@id_info_empleado
END
GO

CREATE PROCEDURE InsertarInformacionEmpleadoEducacion
	@fecha_expedicion DATE, @fecha_caducidad DATE, @id_info_empleado int
AS 
BEGIN
	INSERT INTO Informacion_Empleado_Educacion(fecha_expedicion,fecha_caducidad, id_info_empleado)
	VALUES (@fecha_expedicion, @fecha_caducidad,@id_info_empleado)
END 
GO

CREATE PROCEDURE InsertarMultimedia
	@URL varchar(MAX), @Id_Info_Empleado int
AS 
BEGIN
	INSERT INTO Multimedia(url, id_)
	VALUES (@URL)
END 
GO



CREATE PROCEDURE InsertarEducacion
	@titulo varchar (max), @nombre_institucion varchar (max), @disciplina_academica varchar(max), @actividades_grupo varchar(max), @descripcion varchar(max), @id_info_empleado int
AS
BEGIN
	INSERT INTO Educacion (titulo, nombre_institucion,disciplina_academica,actividades_grupo,descripcion

	
