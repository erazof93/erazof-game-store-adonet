using Erazof.Application.DTOs.Request;
using Erazof.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

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

        // --------- POSTMAN
        [HttpPost("api/login")]
        public async Task<IActionResult> LoginApi([FromBody] LoginRequest model)
        {
            var usuario = await _usuarioService.ValidarUsuario(model);

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas");

            return Ok(usuario);
        }
        //-----------------



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
                    new Claim("UsuarioID", usuario.UsuarioID.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.RolID.ToString()),
                    new Claim("NombreRol", usuario.NombreRol), // Importante para permisos
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 3. Creamos la Cookie en el navegador
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Usuario");
                //return RedirectToAction("Index", "Home");
                
            }

            // Si falla, enviamos un error simple
            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Usuario");
        }


    }
}
