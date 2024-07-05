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
   


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /*REGISTROS Y VERIFICACIONES DE LOGIN*/

    [HttpPost]
    public IActionResult Login(string Mail , string Contraseña)
    {
        const string estilo= "#ABE7DD", foto_perfil="../img/default/photo_default.png", encabezado="encabezado.png", nombre_apellido = "Tu nombre", telefono = "", mail="";
        Usuario Usuario = Models.BD.Login(Mail,Contraseña);
        if (Usuario == null )
        {
            ViewBag.MensajeError = "Usuario o Contraseña Incorrecto";
            return View("Registro");
        }
        else if (Usuario.id_discapacidad == 1)
        {
            BD.user = Usuario;
            Models.BD.CargaPerfilDefault(Usuario, estilo, foto_perfil, encabezado, nombre_apellido, telefono, mail);
            ViewBag.nombre_apellido = nombre_apellido;
            ViewBag.estilo = estilo;
            ViewBag.telefono = telefono;
            ViewBag.mail = mail;
            ViewBag.foto_perfil = foto_perfil;
            return View("PerfilLee");
        }
        else  {
            BD.user = Usuario;
            return RedirectToAction("PerfilNoLee");
        }
    }
    [HttpPost]
    public IActionResult RegistrarUsuario(Usuario user){
        Usuario userr = BD.Registro_VerificarExistencia(user.mail);
        if(userr == null){
           BD.Registro(user.mail, user.contraseña, user.id_discapacidad);
            if(user.id_discapacidad==1) return View("PerfilLee");
            else return View("PerfilNoLee");
        }
        else{
            ViewBag.MSJError= "El usuario ya existe!";
            return View("RegistrarView");
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
}