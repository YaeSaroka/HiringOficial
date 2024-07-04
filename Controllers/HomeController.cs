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
   
   public IActionResult RegistrarView()
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
        Usuario Usuario = Models.BD.Login(Mail,Contraseña);
        if (Usuario == null )
        {
            ViewBag.MensajeError = "Usuario o Contraseña Incorrecto";
            return View("RegistrarView");
        }
        else
        {
            BD.user = Usuario;
            return RedirectToAction("Index");
        }
    }
    [HttpPost]
    public IActionResult RegistrarUsuario(Usuario user){
        Usuario userr = BD.Registro_VerificarExistencia(user.mail);
        if(userr == null){
           BD.Registro(user.mail, user.contraseña);
            return View("Index");
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