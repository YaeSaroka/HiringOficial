using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hiring.Models;


namespace Hiring.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        return View();
    }
      
    public IActionResult Privacy()
    {
        return View();
    }
   
   public IActionResult Registro()
    {
        return View();
    }
   public IActionResult Login()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /*REGISTROS Y VERIFICACIONES DE LOGIN*/

    [HttpPost]
    public IActionResult Login(string Mail , string Contraseña)
    {
        Usuario usuario = Models.BD.Login(Mail,Contraseña); 
        if (usuario == null )
        {
            ViewBag.MensajeError = "Usuario o Contraseña Incorrecto";
            return View("Index");
        }
        else if (usuario.id_discapacidad == 1)
        {
            Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(usuario.id);
            return View("PerfilLee", perfil);
        }
        else  {
            BD.user = usuario;
            return RedirectToAction("PerfilNoLee");
        }
    }
    [HttpPost]
    public IActionResult RegistrarUsuario(Usuario user){
        const string estilo= "#ABE7DD", foto_perfil="../img/default/foto_Default.png", encabezado="encabezado.png", nombre_apellido = "Tu nombre", telefono = "", mail="";
        Usuario userr = BD.Registro_VerificarExistencia(user.mail);

        if(userr == null)
        {
            BD.Registro(user.mail, user.contraseña, user.id_discapacidad);
            userr = BD.Registro_VerificarExistencia(user.mail);

            if(user.id_discapacidad==1) {
                Models.BD.CargaPerfilDefault(userr,estilo, foto_perfil, encabezado, nombre_apellido, telefono, mail);
                Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(userr.id);
                return View("PerfilLee", perfil);
            }
            else return View("PerfilNoLee");
        }
        else
        {
            ViewBag.MSJError= "El usuario ya existe!";
            return View("Registro");
        }
    }

    public IActionResult OlvidarContraseña(string Mail)
    {
        string contraseña = BD.OlvideMiContraseña(Mail);
        if(contraseña == null || contraseña == "") {
            ViewBag.MensajeInexistente = "No existe el mail ingresado anteriormente";
            return View("OlvideContraseña");
        }
        else
        {
            ViewBag.ContraseñaRecordada = contraseña;
            return View("OlvideContraseña");
        }
    }
 
 [HttpPost]
    public IActionResult InsertarInformacionPersonal1(Informacion_Personal_Empleado usuario)
    {
        Models.BD.InsertarInformacionPersonalEmpleado1(usuario);
        Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(usuario.id);
        return View("PerfilLee", perfil);
        }


    [HttpPost]
    public IActionResult InsertarInformacionPersonal2( Informacion_Personal_Empleado usuario)
    {
        Models.BD.InsertarInformacionPersonalEmpleado2(usuario);
        Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(usuario.id);
        return View("PerfilLee", perfil);
    }
    
}