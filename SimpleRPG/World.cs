using ConsoleApp1.Core;
using ConsoleApp1.GameObjects.Characters;
using ConsoleApp1.GameObjects.Core;
using System.IO;

namespace ConsoleApp1
{
    class World
    {
        private const string EOF = "eof";
        private const string END = "end";
        private readonly GODataBase dataBase;
        private readonly Room[,] rooms = new Room[32, 32];
        public Player player;
        private readonly string pathWorld;

        public World(string pathWorld, GODataBase dataBase)
        {
            this.pathWorld = pathWorld;
            this.dataBase = dataBase;

            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    rooms[y, x] = new Room();
                }
            }
        }

        public void Load()
        {
            string[] files = GetFiles();
            int RoomX, RoomY;
            string line;
            StreamReader worldReader;

            foreach (string file in files)
            {
                if (file.Contains("room"))
                {
                    worldReader = new StreamReader(pathWorld + @"\" + file);
                    line = file[5..^4];
                    RoomX = int.Parse(line.Split("_")[0]);
                    RoomY = int.Parse(line.Split("_")[1]);

                    LoadRoom(worldReader, rooms[RoomY, RoomX]);

                    while ((line = worldReader.ReadLine()) != EOF)
                    {
                        switch (line)
                        {
                            case "NPC": rooms[RoomY, RoomX].AddNPC(LoadNPC(worldReader)); break;
                            case "TRIGGER": rooms[RoomY, RoomX].AddTrigger(LoadTrigger(worldReader)); break;
                        }
                    }

                    worldReader.Close();
                }
            }

            CleanRooms();
        }

        private void CleanRooms()
        {
            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    if (rooms[y, x].GetParentWorld() == null) rooms[y, x] = null;
                }
            }
        }

        private NPC LoadNPC(StreamReader worldReader)
        {
            string line, key, val;
            NPC newInstant = new NPC();
            int x, y;

            while ((line = worldReader.ReadLine()) != END)
            {
                key = line.Split('=')[0];
                val = line.Split('=')[1];

                x = val.Contains(':') ? int.Parse(val.Split(':')[0]) : 0;
                y = val.Contains(':') ? int.Parse(val.Split(':')[1]) : 0;

                switch (key)
                {
                    case "ID": newInstant.SetID(int.Parse(val)); break;
                    case "name": newInstant.SetName(val); break;
                    case "Class": newInstant.SetRole(val); break;
                    case "position": newInstant.SetPosition(x, y); break;
                    case "graphics": newInstant.SetGraphics(val); break;

                    default: break;
                }
            }

            return newInstant;
        }

        private Trigger LoadTrigger(StreamReader worldReader)
        {
            Trigger newInstant = new Trigger();

            string line, key, val;
            int x, y;

            while ((line = worldReader.ReadLine()) != END)
            {
                key = line.Split("=")[0];
                val = line.Split("=")[1];

                x = val.Contains(':') ? int.Parse(val.Split(':')[0]) : 0;
                y = val.Contains(':') ? int.Parse(val.Split(':')[1]) : 0;

                switch (key)
                {
                    case "ID": newInstant.SetID(int.Parse(val)); break;
                    case "name": newInstant.SetName(val); break;
                    case "graphics": newInstant.SetGraphics(val); break;
                    case "position": newInstant.SetPosition(x, y); break;
                    case "remoteRoom": newInstant.SetRemoteRoom(GetRoomAt(x, y)); break;
                    case "remotePosition": newInstant.SetRemotePosition(x, y); break;
                }
            }

            return newInstant;
        }

        public string RenderRoom(Room currentRoom)
        {
            string outline = "";

            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 32; x++)
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

        private void LoadRoom(StreamReader worldReader, Room currentRoom)
        {
            int[] parsedLine;
            for (int y = 0; y < 32; y++)
            {
                parsedLine = StringArrayToInt(worldReader.ReadLine().Split(","));
                for (int x = 0; x < 32; x++)
                {
                    currentRoom.SetStaticAt(x, y, dataBase.GetByID(parsedLine[x]).Clone());
                }
            }

            currentRoom.SetParentWorld(this);
        }

        public Room GetRoomAt(Point position)
        {
            return rooms[position.Y, position.X];
        }

        public ref Room GetRoomAt(int x, int y)
        {
            return ref rooms[y, x];
        }

        private int[] StringArrayToInt(string[] stringArray)
        {
            int[] parsed = new int[stringArray.Length];

            for (int i = 0; i < stringArray.Length; i++)
            {
                parsed[i] = int.Parse(stringArray[i]);
            }

            return parsed;
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
