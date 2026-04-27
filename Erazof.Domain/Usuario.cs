using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaReg { get; set; }
        public Rol RolID { get; set; }
    }
}
