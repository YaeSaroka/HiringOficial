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
CREATE PROCEDURE InsertarEducacion
	@titulo varchar (max), @nombre_institucion varchar (max), @disciplina_academica varchar(max), @actividades_grupo varchar(max), @descripcion varchar(max), @fecha_expedicion DATE, @fecha_caducidad DATE
AS
BEGIN

	INSERT INTO Educacion (titulo, nombre_institucion, disciplina_academica, actividades_grupo, descripcion, fecha_expedicion, fecha_caducidad )
    VALUES (@titulo, @nombre_institucion, @disciplina_academica, @actividades_grupo, @descripcion, @fecha_expedicion, @fecha_caducidad);
end