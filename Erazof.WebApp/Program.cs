using Erazof.Application.Interfaces;
using Erazof.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IJuegoService, JuegoService>();

builder.Services.AddControllersWithViews();





// Añadir el servicio de autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Autenticacion/Login"; // A donde redirigir si no está logueado
        options.AccessDeniedPath = "/Autenticacion/AccessDenied";
    });


var app = builder.Build();

app.UseAuthentication(); // ¿Quién eres?
app.UseAuthorization();  // ¿A qué tienes permiso?

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Index}/{id?}");


app.Run();
