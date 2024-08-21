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
        List<string> UrlMultimedia = new List<string>(); 
        Usuario usuario = Models.BD.Login(Mail,Contraseña); 
        if (usuario == null )
        {
            ViewBag.MensajeError = "Usuario o Contraseña Incorrecto";
            return View("Index");
        }
        else if (usuario.id_discapacidad == 1)
        {
            Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(usuario.id);
            ViewBag.UrlMultimedia=Models.BD.SelectMultimedia(usuario.id);
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
            Models.BD.Registro(user.mail, user.contraseña, user.id_discapacidad);
            userr = Models.BD.Registro_VerificarExistencia(user.mail);

            if(user.id_discapacidad==1) {
                Models.BD.CargaPerfilDefault(userr,estilo, foto_perfil, encabezado, nombre_apellido, telefono, mail);
                Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(userr.id);
                //ViewBag.Lista_educacion = Models.BD.SelectEducacion(userr.id);
                // Cargo aca un ViewBag con el listado de Educacion
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
        List<string> UrlMultimedia = new List<string>(); 
        ViewBag.UrlMultimedia=Models.BD.SelectMultimedia(usuario.id);
        return View("PerfilLee", perfil);
        }


    [HttpPost]
    public IActionResult InsertarInformacionPersonal2(Informacion_Personal_Empleado usuario)
    {
        Models.BD.InsertarInformacionPersonalEmpleado2(usuario);
        Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(usuario.id);
        List<string> UrlMultimedia = new List<string>(); 
        ViewBag.UrlMultimedia=Models.BD.SelectMultimedia(usuario.id);
        return View("PerfilLee", perfil);
    }

    [HttpPost]
    public IActionResult InsertarEducacion( Educacion educacion, Informacion_Personal_Empleado usuario){
        Models.BD.InsertarEducacion(educacion);
        Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(usuario.id);
        List<Educacion> Lista_educacion = new List<Educacion>(); 
        ViewBag.Lista_educacion =Models.BD.SelectEducacion(usuario.id);
        return View("PerfilLee", perfil);
    }


   [HttpPost]
public async Task<IActionResult> UploadFile(IFormFile file, int Id_Empleado)
{
    if (file == null || file.Length == 0)
    {
        return Json(new { success = false, message = "No file uploaded." });
    }

    try
    {
       
        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
        
        string extension = Path.GetExtension(file.FileName).ToLower();
        string timeStamp =DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + extension;
        var filePath = Path.Combine(uploads, timeStamp);
       
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Guarda la URL del archivo en la base de datos
        var fileUrl = Url.Content($"/uploads/{timeStamp}"); // Convertir a URL absoluta
        BD.InsertarMultimedia(fileUrl, Id_Empleado);

        //  todas las URLs de multimedia
        var UrlMultimedia = BD.SelectMultimedia(Id_Empleado);

        return Json(new { success = true, data = UrlMultimedia });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}

    
}