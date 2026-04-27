using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("Imagenes")]
    public class Imagen
    {
        public int ImagenID { get; set; }
        public string Url { get; set; }
        public Juego IDJuego { get; set; }
        public bool Portada { get; set; }
    }
}
