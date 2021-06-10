using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = DBUtils.GetDBConnection();
            Console.WriteLine("Openning Connection ...");
            conn.Open();
            Console.WriteLine("Connection successful!");
            int choice = -1;

            try
            {
                do
                {
                    Console.WriteLine("Please, select the function you want:");
                    Console.WriteLine("1) Get data.");
                    Console.WriteLine("2) Insert data.");
                    Console.WriteLine("3) Update data.");
                    Console.WriteLine("4) Delete data.");
                    Console.WriteLine("5) Show character's statistics.");
                    Console.WriteLine("6) Count average character's experience.");
                    Console.WriteLine("0) Close the connection.");
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            GetData(conn);
                            break;
                        case 2:
                            InsertData(conn);
                            break;
                        case 3:
                            UpdateData(conn);
                            break;
                        case 4:
                            DropData(conn);
                            break;
                        case 5:
                            showCharacterStatistics(conn);
                            break;
                        case 6:
                            averageExp(conn);
                            break;
                        default:
                            break;
                    }
                } while (choice != 0);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                // Закрыть соединение.
                conn.Close();
                // Уничтожить объект, освободить ресурс.
                conn.Dispose();
            }
            Console.Read();
        }

        private static void GetData(MySqlConnection conn)
        {
            Console.WriteLine("Please, select the table you want to work with:");
            Console.WriteLine("1) user");
            Console.WriteLine("2) character");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    string sql = "Select user.UserID, user.email, user.password, user.name, gamestatus.Name as Status from user inner join gamestatus where user.StatusID = gamestatus.StatusID LIMIT 0, 1000";

                    // Создать объект Command.
                    MySqlCommand cmd = new MySqlCommand();

                    // Сочетать Command с Connection.
                    cmd.Connection = conn;
                    cmd.CommandText = sql;


                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                // Индекс (index) столбца Emp_ID в команде SQL.
                                int userID_index = reader.GetOrdinal("UserID");
                                int UserID = Convert.ToInt32(reader.GetValue(userID_index));

                                int email_index = reader.GetOrdinal("email");
                                string email = reader.GetString(email_index);

                                int password_index = reader.GetOrdinal("password");
                                string password = reader.GetString(password_index);

                                int name_index = reader.GetOrdinal("name");
                                string name = reader.GetString(name_index);

                                int StatusID_index = reader.GetOrdinal("Status");
                                string StatusID = reader.GetString(StatusID_index);

                                Console.WriteLine("--------------------");
                                Console.WriteLine("UserID: " + UserID);
                                Console.WriteLine("email: " + email);
                                Console.WriteLine("password: " + password);
                                Console.WriteLine("name: " + name);
                                Console.WriteLine("StatusID: " + StatusID);
                                Console.WriteLine("--------------------");
                            }
                        }
                    }
                    break;
                case 2:
                    sql = "select _character.CharacterID, _character.Name AS `Character Name`, _character.Capacity, _character.Health, _character.Experience, _character.Coordinates, location.worldName AS `World`, user.Name AS `Owner` from _character inner join location inner join user where _character.UserID = user.UserID and _character.WorldID = location.WorldID;";

                    // Создать объект Command.
                    cmd = new MySqlCommand();

                    // Сочетать Command с Connection.
                    cmd.Connection = conn;
                    cmd.CommandText = sql;


                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                // Индекс (index) столбца Emp_ID в команде SQL.
                                int CharacterID_index = reader.GetOrdinal("CharacterID");
                                int CharacterID = Convert.ToInt32(reader.GetValue(CharacterID_index));

                                int name_index = reader.GetOrdinal("Character Name");
                                string name = reader.GetString(name_index);

                                int capacity_index = reader.GetOrdinal("Capacity");
                                int capacity = Convert.ToInt32(reader.GetValue(capacity_index));

                                int health_index = reader.GetOrdinal("Health");
                                int health = Convert.ToInt32(reader.GetValue(health_index));

                                int exp_index = reader.GetOrdinal("Experience");
                                int exp = Convert.ToInt32(reader.GetValue(exp_index));

                                int coord_index = reader.GetOrdinal("Coordinates");
                                int coord = Convert.ToInt32(reader.GetValue(coord_index));

                                int world_index = reader.GetOrdinal("World");
                                string world = reader.GetString(world_index);

                                int owner_index = reader.GetOrdinal("Owner");
                                string owner = reader.GetString(owner_index);

                                Console.WriteLine("--------------------");
                                Console.WriteLine("CharacterID: " + CharacterID);
                                Console.WriteLine("Character Name: " + name);
                                Console.WriteLine("Capacity: " + capacity);
                                Console.WriteLine("Health: " + health);
                                Console.WriteLine("Experience: " + exp);
                                Console.WriteLine("Coordinates: " + coord);
                                Console.WriteLine("World: " + world);
                                Console.WriteLine("Owner: " + owner);
                                Console.WriteLine("--------------------");
                            }
                        }
                    }
                    break;
            }        
        }

        private static void InsertData(MySqlConnection conn)
        {
            int choice = -1;
            Console.WriteLine("Please, select the table you want to work with:");
            Console.WriteLine("1) user.");
            Console.WriteLine("2) character.");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    // Команда Insert.
                    string sql = "Insert into user (UserID, email, password, name, StatusID) "
                                                     + " values (@UserID, @email, @password, @name, @StatusID) ";

                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;

                    Console.WriteLine("Введите значения входных параметров: UserID, email, password, name и StatusID");
                    int input_UserID = Convert.ToInt32(Console.ReadLine());
                    string input_email = Console.ReadLine();
                    string input_password = Console.ReadLine();
                    string input_name = Console.ReadLine();
                    int input_StatusID = Convert.ToInt32(Console.ReadLine());

                    // Создать объект Parameter.
                    MySqlParameter UserID = new MySqlParameter("@UserID", SqlDbType.Int);
                    UserID.Value = input_UserID;
                    cmd.Parameters.Add(UserID);

                    // Добавить параметр @email (Написать кратко).
                    MySqlParameter email = cmd.Parameters.Add("@email", MySqlDbType.Text);
                    email.Value = input_email;

                    // Добавить параметр @password (Написать кратко).
                    cmd.Parameters.Add("@password", MySqlDbType.Text).Value = input_password;

                    cmd.Parameters.Add("@name", MySqlDbType.Text).Value = input_name;

                    MySqlParameter StatusID = new MySqlParameter("@StatusID", SqlDbType.Int);
                    StatusID.Value = input_StatusID;
                    cmd.Parameters.Add(StatusID);

                    // Выполнить Command (использованная для  delete, insert, update).
                    int rowCount = cmd.ExecuteNonQuery();

                    Console.WriteLine("Row Count affected = " + rowCount);
                    break;
                case 2:
                    // Команда Insert.
                    sql = "Insert into _character (CharacterID, Name, Capacity, Health, Experience, Coordinates, WorldID, UserID) "
                    + " values (@CharacterID, @Name, @Capacity, @Health, @Experience, @Coordinates, @WorldID, @UserID) ";

                    cmd = conn.CreateCommand();
                    cmd.CommandText = sql;

                    Console.WriteLine("Please, enter the input parameters: CharacterID, Name, Capacity, Health, Experience, Coordinates, WorldID and UserID");
                    int input_CharacterID = Convert.ToInt32(Console.ReadLine());
                    input_name = Console.ReadLine();
                    int input_Capacity = Convert.ToInt32(Console.ReadLine());
                    int input_Health = Convert.ToInt32(Console.ReadLine());
                    int input_Experience = Convert.ToInt32(Console.ReadLine());
                    int input_Coordinates = Convert.ToInt32(Console.ReadLine());
                    int input_WorldID = Convert.ToInt32(Console.ReadLine());
                    input_UserID = Convert.ToInt32(Console.ReadLine());

                    // Создать объект Parameter.
                    MySqlParameter CharactedID = new MySqlParameter("@CharacterID", SqlDbType.Int);
                    CharactedID.Value = input_CharacterID;
                    cmd.Parameters.Add(CharactedID);

                    // Добавить параметр @email (Написать кратко).
                    MySqlParameter name = cmd.Parameters.Add("@Name", MySqlDbType.Text);
                    name.Value = input_name;

                    // Добавить параметр @password (Написать кратко).
                    cmd.Parameters.Add("@Capacity", MySqlDbType.Int32).Value = input_Capacity;

                    cmd.Parameters.Add("@Health", MySqlDbType.Int32).Value = input_Health;
                    cmd.Parameters.Add("@Experience", MySqlDbType.Int32).Value = input_Experience;
                    cmd.Parameters.Add("@Coordinates", MySqlDbType.Int32).Value = input_Coordinates;
                    cmd.Parameters.Add("@WorldID", MySqlDbType.Int32).Value = input_WorldID;
                    cmd.Parameters.Add("@UserID", MySqlDbType.Int32).Value = input_UserID;

                    // Выполнить Command (использованная для  delete, insert, update).
                    rowCount = cmd.ExecuteNonQuery();

                    Console.WriteLine("Row Count affected = " + rowCount);
                    break;
            }
            
        }
        private static void UpdateData(MySqlConnection conn)
        {
            int choice = -1;
            Console.WriteLine("Please, select the table you want to work with:");
            Console.WriteLine("1) user.");
            Console.WriteLine("2) character.");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    string sql = "Update user set password = @password where Name = @name";

                    Console.WriteLine("Please, enter the username and new password to change.");
                    string name = Console.ReadLine();
                    string password = Console.ReadLine();

                    MySqlCommand cmd = new MySqlCommand();

                    cmd.Connection = conn;

                    cmd.CommandText = sql;

                    // Добавить и настроить значение для параметра.
                    cmd.Parameters.Add("@password", MySqlDbType.Text).Value = password;
                    cmd.Parameters.Add("@name", MySqlDbType.Text).Value = name;

                    // Выполнить Command (Использованная для delete, insert, update).
                    int rowCount = cmd.ExecuteNonQuery();

                    Console.WriteLine("Row Count affected = " + rowCount);
                    break;
                case 2:
                    sql = "Update _character set Name = @name where UserID = @UserID";

                    Console.WriteLine("Please, enter the Character Name and owner's UserID to change.");
                    name = Console.ReadLine();
                    int UserID = Convert.ToInt32(Console.ReadLine());

                    cmd = new MySqlCommand();

                    cmd.Connection = conn;

                    cmd.CommandText = sql;

                    // Добавить и настроить значение для параметра.
                    cmd.Parameters.Add("@UserID", MySqlDbType.Int32).Value = UserID;
                    cmd.Parameters.Add("@name", MySqlDbType.Text).Value = name;

                    // Выполнить Command (Использованная для delete, insert, update).
                    rowCount = cmd.ExecuteNonQuery();

                    Console.WriteLine("Row Count affected = " + rowCount);
                    break;
            }
        }
        private static void DropData(MySqlConnection conn)
        {
            int choice = -1;
            Console.WriteLine("Please, select the table you want to work with:");
            Console.WriteLine("1) user.");
            Console.WriteLine("2) character.");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Please, enter the UserID to delete.");
                    int UserID = Convert.ToInt32(Console.ReadLine());

                    // Создать объект Command.
                       
                    string sql = "Delete from _character where UserID = @UserID";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@UserID", MySqlDbType.Int32).Value = UserID;
                    int rowCount = cmd.ExecuteNonQuery();

                    sql = "Delete from user where UserID = @UserID";
                    cmd.CommandText = sql;
                    // Выполнить команду Command (Использованная для delete,insert, update).
                    rowCount = cmd.ExecuteNonQuery();

                    Console.WriteLine("Row Count affected = " + rowCount);
                    break;
                case 2:
                    Console.WriteLine("Please, enter the Character Name to delete.");
                    string characterName = Console.ReadLine();

                    // Создать объект Command.

                    sql = "Delete from _character where Name = @characterName";
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@characterName", MySqlDbType.Text).Value = characterName;
                    rowCount = cmd.ExecuteNonQuery();

                    Console.WriteLine("Row Count affected = " + rowCount);
                    break;
            }
        }
        private static void showCharacterStatistics(MySqlConnection conn)
        {
            Console.WriteLine("Please, enter the Character Name to get statistics.");
            string name = Console.ReadLine();

            // Создать объект Command.

            string sql = "select statistics.name as Parameter, _character.Name as Charname, statstocharacter.Value from statstocharacter inner join statistics on statstocharacter.StatID = statistics.StatID inner join _character on statstocharacter.CharacterID = _character.CharacterID where _character.Name = @name";
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@name", MySqlDbType.Text).Value = name;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        // Индекс (index) столбца Emp_ID в команде SQL.
                        int parameter_index = reader.GetOrdinal("Parameter");
                        string parameter = reader.GetString(parameter_index);

                        int charname_index = reader.GetOrdinal("Charname");
                        string charname = reader.GetString(charname_index);

                        int value_index = reader.GetOrdinal("Value");
                        int value = Convert.ToInt32(reader.GetString(value_index));

                        Console.WriteLine("--------------------");
                        Console.WriteLine("Parameter: " + parameter);
                        Console.WriteLine("Charname: " + charname);
                        Console.WriteLine("Value: " + value);
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
        private static void averageExp(MySqlConnection conn)
        {
            string sql = "select avg(Experience) as Result from _character";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        // Индекс (index) столбца Emp_ID в команде SQL.
                        int result_index = reader.GetOrdinal("Result");
                        double result = Convert.ToDouble(reader.GetValue(result_index));

                        Console.WriteLine("--------------------");
                        Console.WriteLine("Result: " + result);
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
    }
}
