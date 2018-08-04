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

        public static SqlConnection connection = new SqlConnection();

        public static void Connect(string Server, string InitialCatalog, string ID, string Password)
        {
            try
            {
                Dispose(connection);
                string ConnectionStrings = GetConnectionString(Server,InitialCatalog,ID,Password);
                connection.ConnectionString = ConnectionStrings;
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }
        public static void Disconnect()
        {
            Dispose(connection);
        }
        public static SqlConnection GetConnection()
        {
                return connection;
        }

 
        //-- code to make sure to close connection and dispose the object
        public static void Dispose(SqlConnection connection)
        {
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
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
        public static SqlCommand PrepareCommand(SqlConnection connection, string commandName, CommandType commandType, Dictionary<string, string> Parameters)
        {
            SqlCommand cmd = new SqlCommand(commandName, connection);
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
