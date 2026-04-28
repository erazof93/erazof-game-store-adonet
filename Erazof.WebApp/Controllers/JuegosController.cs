using Erazof.Application.Interfaces;
using Erazof.Application.Services;
using Erazof.Domain;
using Erazof.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Erazof.WebApp.Controllers
{
    public class JuegosController : Controller
    {
        private readonly IJuegoService _juegoService;
        public JuegosController()
        {
            _juegoService = new JuegoService();
        }

        public async Task<IActionResult> Genero()
        {
            var genero = await _juegoService.listarGenero();
            return Ok(genero);
        }





        [HttpGet]
        public async Task<IActionResult> Insertar()
        {
            JuegoImagen model = new JuegoImagen();
            model.Generos = await _juegoService.listarGenero();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar(JuegoImagen juego, IFormFile imagen)
        {
            try
            {
                if (imagen != null && imagen.Length > 0)
                {
                    // 1. Generamos un nombre único antes de insertar para enviárselo al SP
                    string extension = Path.GetExtension(imagen.FileName);
                    // Usamos un Guid o un Timestamp para que sea único antes de tener el ID
                    string nombreArchivo = $"{Guid.NewGuid()}{extension}";
                    string rutaRelativa = $"/imagenes/titulos/{nombreArchivo}";

                    // Seteamos la URL en el objeto que va al DALC
                    juego.url = rutaRelativa;

                    // 2. Insertamos en la base de datos (esto ejecuta tu SP que llena Juegos e Imagenes)
                    int idGenerado = await _juegoService.insertarJuegoImagen(juego);

                    if (idGenerado > 0)
                    {
                        if (juego.GenerosSeleccionados != null)
                        {
                            foreach (var generoId in juego.GenerosSeleccionados)
                            {
                                await _juegoService.insertarJuegoGenero(idGenerado, generoId);
                            }
                        }


                        // 3. Si la base de datos respondió bien, guardamos el archivo físico
                        string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes/titulos", nombreArchivo);

                        // Asegurarse de que la carpeta exista
                        string directorio = Path.GetDirectoryName(carpeta);
                        if (!Directory.Exists(directorio)) Directory.CreateDirectory(directorio);

                        using (var stream = new FileStream(carpeta, FileMode.Create))
                        {
                            await imagen.CopyToAsync(stream);
                        }

                        // Si todo salió bien, redirigimos
                        return RedirectToAction("Index", "Usuario");
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo insertar el juego en la base de datos.";
                    }
                }
                else
                {
                    ViewBag.Error = "Debes seleccionar una imagen obligatoriamente.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error: " + ex.Message;
            }

            // Si llegamos aquí es porque algo falló, devolvemos a la vista con el objeto
            return View(juego);

        }


        [HttpGet]
        public async Task<IActionResult> Biblioteca()
        {
            var biblioteca = await _juegoService.listaBibliotecaJuegos();
            return Ok(biblioteca);
        }
    }
}
