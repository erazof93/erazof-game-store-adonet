using Erazof.Application.DTOs.Request;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioDALC _usuarioDalc;
        private readonly JuegosDALC _juegosDalc;
        public UsuarioService()
        {
            _usuarioDalc = new UsuarioDALC();
            _juegosDalc = new JuegosDALC();
        }

        public Task<bool> Actualizar(Usuario usuario)
        {
            return _usuarioDalc.actualizarUsuario(usuario);
        }

        public Task<int> Crear(Usuario usuario)
        {
            return _usuarioDalc.insertarUsuario(usuario);
        }

        public Task<bool> Eliminar(int id)
        {
            return _usuarioDalc.eliminarUsuario(id);
        }

        public Task<List<Usuario>> ListarTodos()
        {
            return _usuarioDalc.listarUsuario();
        }

        public Task<Usuario> ObtenerPorId(int id)
        {
            return _usuarioDalc.listarUsuarioPorID(id);
        }

        public async Task<UsuarioResponse> ValidarUsuario(LoginRequest request)
        {
            // 1. Llamo al DALC
            var user = await _usuarioDalc.obtenerPorCredenciales(request.UserOrEmail, request.Password);

            // 2. Si el DALC devuelve null (no existe o clave mal)
            if (user == null) return null;

            // 3. De Entidad "Usuario" a DTO "UsuarioResponse"
            return new UsuarioResponse
            {
                UsuarioID = user.UsuarioID,
                Username = user.Username,
                Email = user.Email,
                RolID = user.RolID.RolID,
                NombreRol = user.RolID.NombreRol
            };
        }
    }
}
