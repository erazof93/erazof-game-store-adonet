using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erazof.Infrastructure
{
    public class DBConexion
    {
        private const string _server = "localhost";
        private const string _database = "Erazof";
        private const string _user = "sa";
        private const string _pass = "AdminPass_2026!";
        private const string _cadena = $"Server={_server};Database={_database};User Id={_user};Password={_pass}; TrustServerCertificate=True;";

        public static SqlConnection obtenerConexion()
        {
            return new SqlConnection(_cadena);
        }
    }
}
