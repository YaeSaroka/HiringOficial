using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;  // Para HttpContext.Session
using Hiring.Models;  // Asegúrate de tener el espacio de nombres correcto para tu modelo Usuario

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Configurar servicios necesarios
        services.AddMvc();  // Asegúrate de tener esta configuración para MVC
        
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Configurar tiempo de expiración de sesión
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configurar el middleware

        app.UseSession();  // Asegúrate de usar el middleware de sesión antes de MVC

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
