using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace projektV2
{
    class DB
    {
        /// <summary>
        /// return easy accessable and updateable string connection
        /// </summary>
        /// <returns></returns>
        public static string CreateConnString()
        { 
            string server = "server=localhost;";
            string port = "port=3306;";
            string username = "username=root;";
            string pass = "password=;";
            string dataBase = "database=users";
            string connString = server + port + username + pass + dataBase;
            return connString;
        }
        public MySqlConnection Connection = new MySqlConnection(CreateConnString());

        /// <summary>
        /// opening connection between client and server
        /// </summary>
        public void openConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        /// <summary>
        /// closing connection between computer and server
        /// </summary>
        public void closeConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// getting connection status from db
        /// </summary>
        /// <returns></returns>
        public MySqlConnection getConnection()
        {
            return Connection;
        }
    }
}
