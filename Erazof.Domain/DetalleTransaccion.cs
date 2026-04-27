using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("DetalleTransaccion")]
    public class DetalleTransaccion
    {
        public int DetalleID { get; set; }
        public Transaccion IDTransaccion { get; set; }
        public Juego IDJuego { get; set; }
        public decimal PrecioPagado { get; set; }

    }
}
