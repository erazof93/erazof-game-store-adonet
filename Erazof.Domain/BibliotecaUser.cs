using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    public class BibliotecaUser
    {
        
        public int JuegoID { get; set; }
        public string Titulo {  get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fechala { get; set; }
        public DateTime FechaCompra { get; set; }


    }
}
