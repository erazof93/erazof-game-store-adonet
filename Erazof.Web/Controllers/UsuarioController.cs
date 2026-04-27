using Erazof.Application.Interfaces;
using Erazof.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Erazof.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService= usuarioService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // ------------------ LISTAR ------------------
        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            var usuario = await _usuarioService.ListarTodos();
            return Ok(usuario);
                
        }

        // ------------------ CREAR ------------------
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                int idGenerado = await _usuarioService.Crear(usuario);
                if (idGenerado > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "No se pudo insertar el auto en la base de datos.";
                }
            }
            return View(usuario);
        }
    }
}
