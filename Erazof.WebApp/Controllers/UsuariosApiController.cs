using Erazof.Application.DTOs.Response;
using Erazof.Application.Interfaces;
using Erazof.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Erazof.WebApp.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosApiController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosApiController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            List<UsuarioDTO> listaUserDTO = new List<UsuarioDTO>();
            var listaUser = await _usuarioService.ListarTodos();
            foreach(var user in listaUser)
            {
                UsuarioDTO userDTO = new UsuarioDTO
                {
                    idUser = user.UsuarioID,
                    usuario = user.Username,
                    email = user.Email,
                    fechaReg = user.FechaReg
                };
                listaUserDTO.Add(userDTO);
            }

            return Ok(listaUserDTO);
        }

    }
}
