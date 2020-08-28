using SimpleRPG.GameObjects.Core;
using System.Collections.Generic;
using System.IO;

namespace SimpleRPG
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

            Static newInstance = new Static();
            newInstance.Load(objectsReader);

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
