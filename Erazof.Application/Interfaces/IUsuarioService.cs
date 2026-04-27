using Erazof.Application.DTOs.Request;
using Erazof.Application.DTOs.Response;
using Erazof.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ListarTodos();
        Task<Usuario> ObtenerPorId(int id);
        Task<int> Crear(Usuario usuario);
        Task<bool> Actualizar(Usuario usuario);
        Task<bool> Eliminar(int id);

        // autenticacion
        Task<UsuarioResponse> ValidarUsuario(LoginRequest request);

        

    }
}
