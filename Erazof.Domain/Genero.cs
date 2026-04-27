using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("Juegos")]
    public class Genero
    {
        public int GeneroID { get; set; }
        public string NombreGenero { get; set; }
    }
}
