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
        }

        public void Connect()
        {
            connectionDataBase.Open();
            commandRequest = connectionDataBase.CreateCommand();
        }

        private string GetProperty(string propertyName, int id)
        {
            _ = commandRequest.Parameters.AddWithValue("$id", id);
            commandRequest.CommandText = @"SELECT " + propertyName + " FROM Static WHERE ID = $id";
            dataReader = commandRequest.ExecuteReader();

            dataReader.Read();
            string s = dataReader.GetString(0);
            dataReader.Close();

            commandRequest.Parameters.Clear();

            return s;
        }

        public GameObject GetObjectByID(int id)
        {
            Console.WriteLine(GetProperty("Name", id));
            Console.WriteLine(GetProperty("Graphics", id));
            Console.WriteLine(GetProperty("isObstacle", id));
            /*_ = commandRequest.Parameters.AddWithValue("$id", id);
            commandRequest.CommandText = @"SELECT Name FROM Static WHERE ID = $id";
            dataReader = commandRequest.ExecuteReader();

            dataReader.Read();
            Console.WriteLine(dataReader.GetString(0));
            dataReader.Close();

            commandRequest.CommandText = @"SELECT Graphics FROM Static WHERE ID = $id";
            dataReader = commandRequest.ExecuteReader();

            dataReader.Read();
            Console.WriteLine(dataReader.GetString(0));
            dataReader.Close();

            commandRequest.CommandText = @"SELECT isObstacle FROM Static WHERE ID = $id";
            dataReader = commandRequest.ExecuteReader();

            dataReader.Read();
            Console.WriteLine(dataReader.GetString(0));
            dataReader.Close();

            commandRequest.Parameters.Clear();*/

            return null;
        }
    }
}
