using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    [Table("Biblioteca")]
    public class Biblioteca
    {
        public Usuario UsuarioID {  get; set; }
        public Juego JuegoID { get; set; }
        public DateTime FechaCompra {  get; set; }

    }
}
