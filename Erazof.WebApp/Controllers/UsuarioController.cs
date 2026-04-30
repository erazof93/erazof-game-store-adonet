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
        public UsuarioController()
        {
            _usuarioService= new UsuarioService(); 
            _juegoService = new JuegoService();
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


        
    }
}
