using SimpleRPG.GameObjects.Core;
using System.Collections.Generic;
using System.IO;

namespace SimpleRPG
{
    /*
     * Этот класс представляет собой базу данных заготовленных объектов. 
     * Считывание объектов (на данный момент лишь статических) производится из файла pathDBStatic
     **/
    class GODataBase
    {
        const string EOF = "eof"; // Строка знаменующая конец файла

        string pathDBStatic = "GODBStatic.godb"; // Путь к файлу

        public List<Static> baseStatic = new List<Static>(); // Список объектов

        public void Load()
        {
            LoadStatic();
        }

        // Этот метод загружает все статические объекты из файла
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

        // Этот метод загружает один статический объект из открытого потока чтения файла,
        // И добавляет его в список статических объектов
        private void LoadStaticObject(StreamReader objectsReader)
        {
            Static newInstance = new Static();
            newInstance.Load(objectsReader, null);

            baseStatic.Add(newInstance);
        }

        // Возвращает ссылку на объект из списка по его ID (Применять осторожно).
        // Если объект с ID не был найден возвращает null
        public Static GetByID(int ID)
        {
            foreach (Static obj in baseStatic)
            {
                if (obj.GetID() == ID) return obj;
            }

            return null;
        }

        // Клонирует объект из списка по его ID
        //
        // ОПАСНО: Может вызвать NullPointerException! 
        // Отработать исключение.
        public Static CloneByID(int ID)
        {
            return GetByID(ID).Clone();
        }
    }
}
