using ConsoleApp1.GameObjects.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class GODataBase
    {
        const string EOF = "eof";

        string pathDBStatic = "GODBStatic.godb";

        public List<Static> baseStatic = new List<Static>();

        public void Load()
        {
            LoadStatic();
        }

        private void LoadStatic()
        {
            StreamReader objectsReader = new StreamReader(pathDBStatic);
            string line;

            while ((line = objectsReader.ReadLine()) != EOF)
            {
                if (line == "Static") LoadStaticObject(objectsReader);
            }

            objectsReader.Close();
        }

        private void LoadStaticObject(StreamReader objectsReader)
        {
            string line, left, right;
            Static newInstance = new Static();

            while ((line = objectsReader.ReadLine()) != "end")
            {
                left = line.Split('=')[0];
                right = line.Split('=')[1];

                switch (left)
                {
                    case "ID": newInstance.SetID(int.Parse(right)); break;
                    case "name": newInstance.SetName(right); break;
                    case "defaultGraphics": newInstance.SetGraphics(right); break;
                    case "isObstacle": newInstance.SetIsObstacle(right); break;

                    default: Console.WriteLine(left); break;
                }
            }

            baseStatic.Add(newInstance);
        }

        public Static GetByID(int ID)
        {
            foreach (Static obj in baseStatic)
            {
                if (obj.GetID() == ID) return obj;
            }

            return null;
        }

        public Static CloneByID(int ID)
        {
            return GetByID(ID).Clone();
        }
    }
}
