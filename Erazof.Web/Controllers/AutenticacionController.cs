using Erazof.Application.DTOs.Request;
using Erazof.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Erazof.Web.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public AutenticacionController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            // 1. Validamos con el servicio
            var usuario = await _usuarioService.ValidarUsuario(model);

            if (usuario != null)
            {
                // 2. Creamos la "identidad" del usuario (Claims)
                var claims = new List<Claim> 
                    
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.NombreRol), // Importante para permisos
                    new Claim("UsuarioID", usuario.UsuarioID.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 3. Creamos la Cookie en el navegador
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            // Si falla, enviamos un error simple
            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


    }
}
