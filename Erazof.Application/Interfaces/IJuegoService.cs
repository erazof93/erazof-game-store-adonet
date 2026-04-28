using Erazof.Application.DTOs.Response;
using Erazof.Domain;
using Erazof.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Application.Interfaces
{
    public interface IJuegoService
    {
        Task<List<Genero>> listarGenero();
        Task<List<Swiper>> listarswiper();
        Task<int> insertarJuego(Juego juego);
        Task<int> insertarJuegoImagen(JuegoImagen ji);
        Task insertarJuegoGenero(int juegoId, int generoId);
        Task<List<BibliotecaUsuario>> listaBibliotecaJuegos();


    }
}
