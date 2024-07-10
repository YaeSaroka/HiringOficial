using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace Hiring.Models;

public  class BD
{
     private static string ConnectionString { get; set; } = @"Server=.;DataBase=Hiring;Trusted_Connection=True;";   
     public static Usuario usuario; //agarra el usuario loggeado


    /*LOGIN + VERIFICACIONES*/
    public static Usuario Login(string pMail, string pContraseña)
    {
        Usuario user = null;
        using(SqlConnection db = new SqlConnection(ConnectionString))
        {
            string sp = "LoginUsuario";
            user= db.QueryFirstOrDefault<Usuario>(sp, new {pMail = pMail, pContraseña = pContraseña }, commandType: CommandType.StoredProcedure);
        }
        return user;
    }
    

    public static Usuario Login_VerificarContraseña(string pContraseña)
    {
       Usuario user = null;
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "Login_VerificarContraseña";
            user= db.QueryFirstOrDefault<Usuario>(sp, new {Contraseña=pContraseña}, commandType: CommandType.StoredProcedure);
        }
        return user;
    }
    
     public static Usuario Registro_VerificarExistencia(string Mail)
    {
       Usuario user = null;
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "Registro_VerificarExistencia";
            user= db.QueryFirstOrDefault<Usuario>(sp, new {Mail = Mail }, commandType: CommandType.StoredProcedure);
        }
        return user;
    }
    
      public static void Registro(string Mail, string Contraseña, int id_discapacidad)
    {
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "Registro";
            var parameters = new { Mail = Mail, Contraseña = Contraseña , id_discapacidad=id_discapacidad};
            db.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public static string OlvideMiContraseña(string pMail)
    {
        string Contraseña_recuperada = " ";
        using(SqlConnection db = new SqlConnection(ConnectionString))
        {
            string sp = "OlvideMiContraseña";
            Contraseña_recuperada= db.QueryFirstOrDefault<string>(sp, new { Mail=pMail }, commandType: CommandType.StoredProcedure);
        }
        return Contraseña_recuperada;
    }

    /*----------------------------------------------------------------*/
    public static void CargaPerfilDefault(Usuario usuario, string estilo, string foto_perfil, string encabezado, string nombre_apellido, string telefono, string mail)
    {
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "CargaPerfilDefault";
            var parameters = new { id = usuario.id, estilo=estilo, foto_perfil=foto_perfil, encabezado=encabezado, nombre_apellido = nombre_apellido, telefono = telefono, mail = mail};
            db.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
        }
    }
     public static Informacion_Personal_Empleado CargarPerfilLogin(int id)
{
     // Inicializamos perfil como null
    Informacion_Personal_Empleado perfil = null;
    using (SqlConnection db = new SqlConnection(ConnectionString))
    {
       
        string sp = "CargarPerfilLogin";
        var parameters = new { id = id };
         perfil = db.QueryFirstOrDefault<Informacion_Personal_Empleado>(sp, parameters, commandType: CommandType.StoredProcedure);
    }

    return perfil;}
    
    public static void InsertarInformacionPersonalEmpleado1(Usuario user, Informacion_Personal_Empleado usuario)
    {
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "InsertarInformacionPersonalEmpleado1";
            var parameters = new { id = user.id, estilo=usuario.estilo, nombre_apellido = usuario.nombre_apellido, telefono = usuario.telefono, mail = usuario.mail};
            db.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public static void InsertarInformacionPersonalEmpleado2(Usuario user, Informacion_Personal_Empleado usuario)
    {
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "InsertarInformacionPersonalEmpleado2";
            var parameters = new { id=user.id, foto_perfil=usuario.foto_perfil, acerca_de_mi=usuario.acerca_de_mi, profesion=usuario.profesion_actual, ubicacion=usuario.ubicacion};
            db.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    
    }

