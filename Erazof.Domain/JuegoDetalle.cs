using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    public class JuegoDetalle
    {
        public int JuegoID { get; set; }
        public string Titulo { get; set; }
        public string Url { get; set; }
        public bool portada { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaLanz { get; set; }
        // Aquí capturaremos los géneros concatenados
        public string Generos { get; set; }
    }
}
