using Erazof.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Application.DTOs.Response
{
    public class UsuarioResponse
    {
        public int UsuarioID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int RolID { get; set; }
        public string NombreRol { get; set; }
    }
}
