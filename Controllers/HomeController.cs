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
    public IActionResult PerfilLee(int id)
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
            ViewBag.Lista_educacion = Models.BD.SelectEducacion(usuario.id);
            ViewBag.Adaptacion = Models.BD.SelectAdaptacion(usuario.id);
            return View("PerfilLee", perfil);
        }
        else  {
            Informacion_Personal_Empleado perfil= Models.BD.CargarPerfilLogin(usuario.id);
            return View("PerfilNoLee", perfil);
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

    public IActionResult OlvideContrasena(string Mail)
    {
        string contraseña = BD.OlvideMiContraseña(Mail);
        if(contraseña == null || contraseña == "") {
            ViewBag.MensajeInexistente = "No existe el mail ingresado anteriormente";
            return View("OlvideContrasena");
        }
        else
        {
            ViewBag.ContraseñaRecordada = contraseña;
            return View("OlvideContrasena");
        }
    }
 
 //INFO PERSONAL
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

//EDUCACION
[HttpPost]
public IActionResult InsertarEducacion(Educacion educacion, int Id_Info_Empleado, int id)
{
    if (id != 0 || id==0)
    {
        Models.BD.InsertarEducacion(educacion, Id_Info_Empleado);
    }
    else
    {
        var educacionExistente = Models.BD.SelectEducacion(Id_Info_Empleado);
        if (educacionExistente.Any(e => e.titulo == educacion.titulo && e.disciplina_academica == educacion.disciplina_academica && e.descripcion == educacion.descripcion)) 
        {
            ViewBag.MensajeError = "Esta educación ya fue añadida.";
            Informacion_Personal_Empleado perfil = Models.BD.CargarPerfilLogin(Id_Info_Empleado);
            ViewBag.Lista_educacion = educacionExistente;
            return View("PerfilLee", perfil);
        }
        Models.BD.InsertarEducacion(educacion, Id_Info_Empleado);
    }
    List<Educacion> Lista_educacion = Models.BD.SelectEducacion(Id_Info_Empleado);
    ViewBag.Lista_educacion = Lista_educacion;
    Informacion_Personal_Empleado perfilActualizado = Models.BD.CargarPerfilLogin(Id_Info_Empleado);
    return View("PerfilLee", perfilActualizado);
}
public IActionResult EliminarEducacion(int Id_Info_Empleado, int id)
{
    try
    {
        Models.BD.EliminarEducacion(id);
        List<Educacion> Lista_educacion = Models.BD.SelectEducacion(Id_Info_Empleado);
        ViewBag.Lista_educacion = Lista_educacion;
        return Json(new { success = true });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = "No se pudo eliminar la educación." });
    }
}

public JsonResult ObtenerDatosEducacion(int id)
{
    var educacion = Models.BD.SelectEducacionIdCard(id);

    if (educacion == null)
    {
        return Json(new { success = false, message = "Educación no encontrada." });
    }
    return Json(new
    {
        success = true,
        id = educacion.id,
        titulo = educacion.titulo,
        nombre_institucion = educacion.nombre_institucion,
        disciplina_academica = educacion.disciplina_academica,
        actividades_grupo = educacion.actividades_grupo,
        descripcion = educacion.descripcion,
        fecha_expedicion = educacion.fecha_expedicion,
        fecha_caducidad = educacion.fecha_caducidad,
        mes_expedicion = educacion.mes_expedicion,
        mes_caducidad = educacion.mes_caducidad
    });
}

//MULTIMEDIA
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
        string timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + extension;
        var filePath = Path.Combine(uploads, timeStamp);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        // Guarda la URL del archivo en la base de datos
        var fileUrl = Url.Content($"/uploads/{timeStamp}"); // URL absoluta
        BD.InsertarMultimedia(fileUrl, Id_Empleado);
        var UrlMultimedia = BD.SelectMultimedia(Id_Empleado);
        return Json(new { success = true, data = UrlMultimedia });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return Json(new { success = false, message = ex.Message });
    }
}
[HttpPost]
public IActionResult InsertarAdaptacion(Necesidad adaptacion, int Id_Info_Empleado, int id)
{
    if (id != 0 || id==0)
    {
        Models.BD.InsertarAdaptacion(adaptacion, Id_Info_Empleado);
    }
    Necesidad adaptacion_ = Models.BD.SelectAdaptacion(Id_Info_Empleado);
    ViewBag.Adaptacion = adaptacion_;
    Informacion_Personal_Empleado perfilActualizado = Models.BD.CargarPerfilLogin(Id_Info_Empleado);
    return View("PerfilLee", perfilActualizado);
}
public IActionResult EliminarAdaptacion(int Id_Info_Empleado, int id)
{
    try
    {
        Models.BD.EliminarAdaptacion(id);
       Necesidad Adaptacion = Models.BD.SelectAdaptacion(Id_Info_Empleado);
        ViewBag.Adaptacion = Adaptacion;
        return Json(new { success = true });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = "No se pudo eliminar la Adaptacion." });
    }
}
public JsonResult ObtenerDatosAdaptacion(int id)
{
    // Llama al método SelectAdaptacion con el parámetro id
    var necesidad = Models.BD.SelectAdaptacionIdCard(id);

    if (necesidad == null)
    {
        return Json(new { success = false, message = "Adpatacion no encontrada." });
    }
    return Json(new
    {
        success = true,
        id = necesidad.id,
        nombre = necesidad.nombre
    });
}


}