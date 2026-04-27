using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    public class JuegoImagen
    {
        public string titulo {  get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string url { get; set; }

        // NUEVO: géneros seleccionados
        public List<int> GenerosSeleccionados { get; set; }

        // NUEVO: lista para mostrar en la vista
        public List<Genero> Generos { get; set; }

    }
}
