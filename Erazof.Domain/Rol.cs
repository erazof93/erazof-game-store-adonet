using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("Roles")]
    public class Rol
    {
        public int RolID {  get; set; }
        public string NombreRol { get; set; }
    }
}
