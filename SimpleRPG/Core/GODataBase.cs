using SimpleRPG.GameObjects.Core;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SimpleRPG
{
    class GODataBase
    {
        SQLiteConnection connectionDataBase;
        SQLiteCommand commandRequest;

        Dictionary<int, Static> dbStatic = new Dictionary<int, Static>();

        public GODataBase(string pathDB)
        {
            connectionDataBase = new SQLiteConnection("Data Source=" + pathDB);
            Connect();
        }

        public void Connect()
        {
            connectionDataBase.Open();
            commandRequest = connectionDataBase.CreateCommand();

            LoadObjects(commandRequest, dbStatic); // Ахуительно. Бегом пуш пока не проёб

            commandRequest.Cancel();
            connectionDataBase.Close();

            foreach (KeyValuePair<int, Static> s in dbStatic)
            {
                Console.WriteLine(s.Value.ID);
                Console.WriteLine(s.Value.Name);
                Console.WriteLine(s.Value.Graphics);
                Console.WriteLine(s.Value.IsObstacle);
                Console.WriteLine();
            }
        }

        private void LoadObjects<T>(
            SQLiteCommand commandRequest,
            Dictionary<int, T> dbGameObjects) where T : GameObject, new()
        {
            SQLiteDataReader dataReader;
            string typeName = typeof(T).Name.ToString();
            T newInstance;

            RequestObjectType(typeName);
            int recordsCount = GetTableSize(typeName);

            for (int i = 1; i < recordsCount; i++)
            {
                commandRequest.Parameters.AddWithValue("$id", i);
                dataReader = commandRequest.ExecuteReader();

                dataReader.Read();
                newInstance = LoadObject<T>(dataReader);
                dbGameObjects.Add(newInstance.ID, newInstance);
                dataReader.Close();
            }

            commandRequest.Parameters.Clear();
        }

        private int GetTableSize(string table)
        {
            SQLiteCommand comm = connectionDataBase.CreateCommand();
            comm.CommandText = @"SELECT COUNT(*) FROM Static";
            int tableSize = int.Parse(comm.ExecuteScalar().ToString()) + 1;
            comm.Cancel();

            return tableSize;
        }

        private void RequestObjectType(string type)
        {
            commandRequest.CommandText = @"SELECT * FROM " + type + " WHERE ID = $id";
        }

        private T LoadObject<T>(SQLiteDataReader dataReader) where T : GameObject, new()
        {
            T newInstance = new T();
            newInstance.Load(dataReader);
            return newInstance;
        }
    }
}
