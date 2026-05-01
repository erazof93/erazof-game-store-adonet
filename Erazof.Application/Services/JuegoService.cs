using Erazof.Application.DTOs.Response;
using Erazof.Application.Interfaces;
using Erazof.Domain;
using Erazof.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Application.Services
{
    public class JuegoService : IJuegoService
    {
        private readonly JuegosDALC _juegosDalc;
        public JuegoService()
        {
            _juegosDalc = new JuegosDALC();
        }

        public Task agregarJuego(int usuarioID, int juegoID)
        {
            return _juegosDalc.agregarJuego(usuarioID, juegoID);
        }

        public Task<int> insertarJuego(Juego juego)
        {
            return _juegosDalc.insertarJuego(juego);
        }

        public Task insertarJuegoGenero(int juegoId, int generoId)
        {
            return _juegosDalc.insertarJuegoGenero(juegoId, generoId);
        }

        public Task<int> insertarJuegoImagen(JuegoImagen ji)
        {
            return _juegosDalc.insertarJuegoImagen(ji);
        }

        public Task<List<BibliotecaUsuario>> listaBibliotecaJuegos()
        {
            return _juegosDalc.ObtenerBibliotecaUsuario();
        }

        public Task<JuegoDetalle> listaJuegosDetalles(int id)
        {
            return _juegosDalc.ObtenerJuegoPorId(id);
        }

        public Task<List<Genero>> listarGenero()
        {
            return _juegosDalc.listarGenero() ;
        }

        public Task<List<Swiper>> listarswiper()
        {
            return _juegosDalc.listarswiper();
        }

        public Task<List<BibliotecaUser>> ObtenerBibliotecaPorID(int id)
        {
            return _juegosDalc.ObtenerBibliotecaPorID(id);
        }
    }
}
