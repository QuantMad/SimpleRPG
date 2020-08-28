using SimpleRPG.Core;
using SimpleRPG.GameObjects.Characters;
using SimpleRPG.GameObjects.Core;
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
        private const int ROOM_AND_WORLD_SIZE = 32;

        // Хранит ссылку на базу данных статичных объектов
        private readonly GODataBase dataBase;
        // Массив комнат
        private readonly Room[,] rooms = new Room[ROOM_AND_WORLD_SIZE, ROOM_AND_WORLD_SIZE];
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

            for (int y = 0; y < ROOM_AND_WORLD_SIZE; y++)
            {
                for (int x = 0; x < ROOM_AND_WORLD_SIZE; x++)
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
            // Хранит текущую считанную из файла строку 
            string currentLine;
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

                    // Выполняет чтение области 32x32 из файла
                    LoadRoom(worldReader, rooms[RoomY, RoomX]);

                    // Построчно считывает файл до конца
                    while ((currentLine = worldReader.ReadLine()) != EOF)
                    {
                        // Определяет тип загружаемого объекта, и выполняет его загрузку
                        switch (currentLine)
                        {
                            case "NPC": rooms[RoomY, RoomX].AddNPC(LoadNPC(worldReader)); break;
                            case "TRIGGER": rooms[RoomY, RoomX].AddTrigger(LoadTrigger(worldReader)); break;

                            default: break;
                        }
                    }

                    // Закрывает поток чтения из файла
                    worldReader.Close();
                }
            }

            // Удаляет пустые комнаты
            CleanRooms();
        }

        /**
         * Этот метод считывает первые 32 строки файла комнаты, 
         * и создаёт на их базе карту объектов комнаты
         **/
        private void LoadRoom(StreamReader worldReader, Room currentRoom)
        {
            int[] parsedLine;
            for (int y = 0; y < ROOM_AND_WORLD_SIZE; y++)
            {
                parsedLine = StringArrayToInt(worldReader.ReadLine().Split(","));
                for (int x = 0; x < ROOM_AND_WORLD_SIZE; x++)
                {
                    currentRoom.SetStaticAt(x, y, dataBase.GetByID(parsedLine[x]).Clone());
                }
            }

            currentRoom.SetParentWorld(this);
        }

        /**
         * Загружает блок NPC из потока чтения файла
         **/
        private NPC LoadNPC(StreamReader worldReader)
        {
            NPC newInstance = new NPC();
            newInstance.Load(worldReader);
            return newInstance;
        }

        private Trigger LoadTrigger(StreamReader worldReader)
        {
            Trigger newInstance = new Trigger();
            newInstance.Load(worldReader, this);

            return newInstance;
        }

        public string RenderRoom(Room currentRoom)
        {
            string outline = "";

            for (int y = 0; y < ROOM_AND_WORLD_SIZE; y++)
            {
                for (int x = 0; x < ROOM_AND_WORLD_SIZE; x++)
                {
                    if (player.IsObjectAt(x, y))
                    {
                        outline += player.GetGraphics();
                    }
                    else if (currentRoom.IsAnyTriggerAt(x, y))
                    {
                        outline += currentRoom.GetTriggerAt(x, y).GetGraphics();
                    }
                    else if (currentRoom.IsAnyNPCAt(x, y))
                    {
                        outline += currentRoom.GetNPCAt(x, y).GetGraphics();
                    }
                    else
                    {
                        outline += currentRoom.GetStaticAt(x, y).GetGraphics();
                    }
                }
                outline += "\n";
            }

            return outline;
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
            for (int y = 0; y < ROOM_AND_WORLD_SIZE; y++)
            {
                for (int x = 0; x < ROOM_AND_WORLD_SIZE; x++)
                {
                    if (rooms[y, x].GetParentWorld() == null) rooms[y, x] = null;
                }
            }
        }

        private int[] StringArrayToInt(string[] stringArray)
        {
            int[] parsedLine = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                parsedLine[i] = int.Parse(stringArray[i]);
            }

            return parsedLine;
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
