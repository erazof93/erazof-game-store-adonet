using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("Juegos")]
    public class Juego
    {
        public int JuegoID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaLanzamiento { get; set; }
    }
}
