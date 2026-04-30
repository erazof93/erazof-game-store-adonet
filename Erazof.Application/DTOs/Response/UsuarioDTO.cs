using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Application.DTOs.Response
{
    public class UsuarioDTO
    {
        public int idUser { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public DateTime fechaReg { get; set; }
    }
}
