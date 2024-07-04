<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hiring.Models;

public class HomeController : Controller
{
    [HttpPost]
    public IActionResult Login(string Mail, string Contraseña)
    {
        Usuario usuario = BD.Login(Mail, Contraseña);
        
        if (usuario == null)
        {
            ViewBag.MensajeError = "Usuario o Contraseña Incorrecto";
            return View("RegistrarView"); // Redirigir a vista de login
        }
        else
        {
            HttpContext.Session.SetString("UserID", usuario.id.ToString());
            HttpContext.Session.SetString("UserName", usuario.name);

            return RedirectToAction("Index"); // Redirigir a la página de inicio
        }
    }

    [HttpPost]
    public IActionResult RegistrarUsuario(Usuario user)
    {
        Usuario userr = BD.Registro_VerificarExistencia(user.mail);
        
        if (userr == null)
        {
            BD.Registro(user.mail, user.contraseña);
            return RedirectToAction("Index"); // Redirigir a la página de inicio
        }
        else
        {
            ViewBag.MSJError = "El usuario ya existe!";
            return View("RegistrarView"); // Redirigir a vista de registro
        }
    }

    public IActionResult OlvidarContraseña(string Mail)
    {
        string contraseña = BD.OlvideMiContraseña(Mail);
        
        if (contraseña == null || contraseña == "")
        {
            ViewBag.MensajeInexistente = "No existe el mail ingresado anteriormente";
        }
        else
        {
            ViewBag.ContraseñaRecordada = contraseña;
        }
        
        return View("OlvideContraseña");
=======
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
>>>>>>> ed00ea72a6157697bf4f507249b494d34aa1bb91
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
<<<<<<< HEAD
=======

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
>>>>>>> ed00ea72a6157697bf4f507249b494d34aa1bb91
}
