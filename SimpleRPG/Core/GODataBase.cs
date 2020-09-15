using System;
using System.Data.SQLite;

namespace SimpleRPG
{
    class GODataBase
    {
        SQLiteConnection connectionDataBase;
        SQLiteCommand commandRequest;
        SQLiteDataReader dataReader;

        public GODataBase(string pathDB)
        {
            connectionDataBase = new SQLiteConnection("Data Source=" + pathDB);
            Connect();


        }

        public void Connect()
        {
            connectionDataBase.Open();
            commandRequest = connectionDataBase.CreateCommand();
            commandRequest.CommandText = @"SELECT * FROM Static WHERE ID = $id";
            commandRequest.Parameters.AddWithValue("$id", 2);
            dataReader = commandRequest.ExecuteReader();
            dataReader.Read();

            Console.WriteLine("FieldCount: " + dataReader.FieldCount);
            Console.WriteLine("HasRows: " + dataReader.HasRows);
            Console.WriteLine();

            Console.WriteLine("GO class: Static");
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                Console.WriteLine(dataReader.GetOriginalName(i) + " = " + dataReader.GetValue(i));
            }

        }

        private SQLiteDataReader GetProperty(string propertyName, int id)
        {
            _ = commandRequest.Parameters.AddWithValue("$id", id);
            commandRequest.CommandText = @"SELECT " + propertyName + " FROM Static WHERE ID = $id";
            dataReader = commandRequest.ExecuteReader();

            dataReader.Read();
            /*string s = dataReader.GetString(0);
            dataReader.Close();

            commandRequest.Parameters.Clear();*/

            return dataReader;
        }

        public GameObject GetObjectByID(int id)
        {
            Console.WriteLine(GetProperty("Name", id));
            Console.WriteLine(GetProperty("Graphics", id));
            Console.WriteLine(GetProperty("isObstacle", id));

            return null;
        }
    }
}
