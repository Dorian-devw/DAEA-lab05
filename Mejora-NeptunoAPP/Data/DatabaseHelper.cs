using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Mejora_NeptunoAPP.Data
{
    public class DatabaseHelper
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NeptunoConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
