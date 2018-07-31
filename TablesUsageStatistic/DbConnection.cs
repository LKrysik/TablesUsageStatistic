using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablesUsageStatistic
{
    public static class DbConnection
    {


        public static SqlConnection GetConnection(string Server, string InitialCatalog, string ID, string Password)
        {
            try
            {
                string ConnectionStrings = GetConnectionString(Server,InitialCatalog,ID,Password);
                SqlConnection con = new SqlConnection(ConnectionStrings);
                con.Open();
                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
                return null;
            }
        }

        //-- code to make sure to close connection and dispose the object
        public static void Dispose(SqlConnection con)
        {
            try
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
        static private string GetConnectionString(string Server, string InitialCatalog, string ID, string Password)
        {
            string str = "Server=" + Server + ";Initial Catalog=" + InitialCatalog + ";Persist Security Info=False;User ID=" + ID + ";Password=" + Password + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            Console.WriteLine(str);
            return "Server=" + Server + ";Initial Catalog=" + InitialCatalog + ";Persist Security Info=False;User ID=" +ID+ ";Password=" + Password + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        //-- prepares SqlCommand object
        public static SqlCommand PrepareCommand(SqlConnection con, string commandName, CommandType commandType, Dictionary<string, string> Parameters)
        {
            SqlCommand cmd = new SqlCommand(commandName, con);
            cmd.CommandType = commandType;
            if (null != Parameters)
            {
                foreach (var item in Parameters)
                    cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
            }
            return cmd;
        }
    }
}
