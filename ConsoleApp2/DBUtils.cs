using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleApp2
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "mydb";
            string username = "root";
            string password = "TogetherForever27!";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}
