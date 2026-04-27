using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("JuegoGeneros")]
    public class JuegoGenero
    {
        public Juego IDJuego { get; set; }
        public Genero IDGenero { get; set; }
    }
}
