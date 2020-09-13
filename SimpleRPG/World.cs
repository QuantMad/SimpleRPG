using SimpleRPG.Core;
using SimpleRPG.GameObjects.Characters;
using System.IO;

namespace SimpleRPG
{
    // TODO: Разделить функционал загрузки из файлов и кнтейнера?
    // Этот класс является контейнером для комнат. 
    internal class World
    {
        // Константа определяющая конец файла
        private const string EOF = "eof";
        // Размер игровой комнаты и игрового мира
        private const int WORLD_SIZE = 32;

        // Хранит ссылку на базу данных статичных объектов
        private readonly GODataBase dataBase;
        // Массив комнат
        private readonly Room[,] rooms = new Room[WORLD_SIZE, WORLD_SIZE];
        // Ссылка на игрока
        public Player player;
        // Путь к папке с комнатами мира
        private readonly string pathWorld;

        /**
         * Конструктор класса. Принимает путь к директории с файлами комнат, 
         * и ссылку на объект базы данных статических объектов. 
         * Заполняет массив комнат пустыми объектами для последующей работы с ними.
         */
        public World(string pathWorld, GODataBase dataBase)
        {
            this.pathWorld = pathWorld;
            this.dataBase = dataBase;

            for (int y = 0; y < WORLD_SIZE; y++)
            {
                for (int x = 0; x < WORLD_SIZE; x++)
                {
                    rooms[y, x] = new Room();
                }
            }
        }

        /**
         * Производит пофайловую загрузку комнат и содержащихся в них объектов (NPC, Items, и д.р.)
         **/
        public void Load()
        {
            // Хранит список файлов в директории с файлами мира
            string[] files = GetFiles();
            // Позиция X и Y комнаты в мире
            int RoomX, RoomY;

            // Поток чтения файла 
            StreamReader worldReader;

            // Перебирает файлы в папке, и если файл является файлом описания комнаты, 
            // то заполняет соответствущую имени файла комнаты ячейку массива rooms[,]
            foreach (string file in files)
            {
                if (file.Contains("room"))
                {
                    worldReader = new StreamReader(pathWorld + @"\" + file);

                    // Вычленяет из имени файла координаты комнаты на глобальной карте rooms[,]
                    RoomX = int.Parse(file[5..^4].Split("_")[0]);
                    RoomY = int.Parse(file[5..^4].Split("_")[1]);

                    rooms[RoomY, RoomX].SetParentWorld(this);
                    rooms[RoomY, RoomX].Load(worldReader, dataBase);

                    // Закрывает поток чтения из файла
                    worldReader.Close();
                }
            }

            // Удаляет пустые комнаты
            CleanRooms();
        }

        //Legacy
        public string[,] BuildRoomImage(Room currentRoom)
        {
            string[,] currentImage = new string[32, 32];

            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (player.IsObjectAt(x, y))
                    {
                        currentImage[y, x] = player.GetGraphics();
                    }
                    else if (currentRoom.IsAnyTriggerAt(x, y))
                    {
                        currentImage[y, x] = currentRoom.GetTriggerAt(x, y).GetGraphics();
                    }
                    else if (currentRoom.IsAnyNPCAt(x, y))
                    {
                        currentImage[y, x] = currentRoom.GetNPCAt(x, y).GetGraphics();
                    }
                    else
                    {
                        currentImage[y, x] = currentRoom.GetStaticAt(x, y).GetGraphics();
                    }
                }
            }

            return currentImage;
        }

        public Room GetRoomAt(Point position)
        {
            return rooms[position.Y, position.X];
        }

        public ref Room GetRoomAt(int x, int y)
        {
            return ref rooms[y, x];
        }

        /**
         * Этот метод очищает глобальную карту комнат от пустых комнат. 
         * Критерием определения пустой комнаты является отсутствие у неё 
         * родительского объекта World.
         **/
        private void CleanRooms()
        {
            for (int y = 0; y < WORLD_SIZE; y++)
            {
                for (int x = 0; x < WORLD_SIZE; x++)
                {
                    if (rooms[y, x].GetParentWorld() == null) rooms[y, x] = null;
                }
            }
        }

        private string[] GetFiles()
        {
            string[] filenames = Directory.GetFiles(pathWorld);

            for (int i = 0; i < filenames.Length; i++)
            {
                filenames[i] = Path.GetFileName(filenames[i]);
            }
            return filenames;
        }
    }
}
