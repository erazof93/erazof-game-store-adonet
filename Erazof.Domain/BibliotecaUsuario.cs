using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Domain
{
    public class BibliotecaUsuario
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int GameID { get; set; }
        public string titulo { get; set; }
        public decimal precio { get; set; }
        public DateTime FechaCompra { get; set; }
        

    }
}
