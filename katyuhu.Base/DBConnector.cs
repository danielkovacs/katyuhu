using System;
using MySql.Data.MySqlClient;

namespace katyuhu.Base
{
    public class DBConnector
    {
        private string connectionString;
        MySqlConnection connection;

        public DBConnector(string cstr)
        {
            this.connectionString = cstr;
            connection = new MySqlConnection(connectionString);

            try
            {
                Console.WriteLine("connecting to DB...");
                connection.Open();
                Console.WriteLine("Connected to DB successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}
