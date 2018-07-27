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


        public static SqlConnection GetConnection()
        {
            try
            {
                string ConnectionStrings = GetConnectionString();
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
        static private string GetConnectionString()
        {
            return "";
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
