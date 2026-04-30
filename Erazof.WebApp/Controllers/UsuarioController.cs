using Erazof.Application.Interfaces;
using Erazof.Application.Services;
using Erazof.Domain;
using Erazof.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Erazof.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJuegoService _juegoService;
        public UsuarioController(IUsuarioService usuarioService, IJuegoService juegoService)
        {
            _usuarioService = usuarioService; 
            _juegoService = juegoService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Llamamos a ambos métodos del servicio
            var listaGeneros = await _juegoService.listarGenero();
            var listaJuegos = await _juegoService.listarswiper();

            // Llenamos el ViewModel
            var model = new HomeViewModel
            {
                ListaGeneros = listaGeneros,
                ListaSwiper = listaJuegos
            };
            return View(model);
        }

        // ------------------ LISTAR ------------------
        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            var usuario = await _usuarioService.ListarTodos();
            return View(usuario);
                
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
            int idGenerado = await _usuarioService.Crear(usuario);


                return RedirectToAction("Login", "Autenticacion");


        }

        // -----------------Modificar
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var usuario = await _usuarioService.ObtenerPorId(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Usuario use)
        {
            bool actualizado = await _usuarioService.Actualizar(use);
            if (actualizado)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "No se pudo actualizar el usuario. Verifique los datos.");
            }
            return View(use);
        }




    }
}
