using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("Transacciones")]
    public class Transaccion
    {
        public int TransaccionID { get; set; }
        public Usuario IDUsuario { get; set; }
        public decimal Total {  get; set; }
        public DateTime Fecha { get; set; }
    }
}
