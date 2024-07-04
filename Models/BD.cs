using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
namespace Hiring.Models;

public  class BD
{
     private static string ConnectionString { get; set; } = @"Server=.;DataBase=Hiring;Trusted_Connection=True;";
    public static Usuario user;

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
    
     public static Usuario Registro_VerificarExistencia(string Username)
    {
       Usuario user = null;
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "Registro_VerificarExistencia";
            user= db.QueryFirstOrDefault<Usuario>(sp, new {pUsername = Username }, commandType: CommandType.StoredProcedure);
        }
        return user;
    }
    
      public static void Registro(string Mail, string Contraseña)
    {
        using(SqlConnection db = new SqlConnection(ConnectionString)){
            string sp = "Registro";
            var parameters = new { pMail = Mail, pContraseña = Contraseña };
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
    }}
