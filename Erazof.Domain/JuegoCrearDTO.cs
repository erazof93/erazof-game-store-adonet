using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    public class JuegoCrearDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public IFormFile FotoFile { get; set; } // Para recibir el archivo
        public List<int> GenerosSeleccionados { get; set; } // Para recibir los IDs de los checkboxes

    }
}
