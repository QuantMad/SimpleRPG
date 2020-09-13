using SimpleRPG.Core;
using SimpleRPG.GameObjects.Characters;
using SimpleRPG.GameObjects.Core;
using System.Collections.Generic;
using System.IO;

namespace SimpleRPG
{
    class Room
    {
        private readonly Static[,] mapStatic = new Static[32, 32];
        private readonly List<NPC> listNPC = new List<NPC>();
        private readonly List<Trigger> listTriggers = new List<Trigger>();
        private World parentWorld;

        public void Load(StreamReader roomReader, GODataBase dataBase)
        {
            LoadStatic(roomReader, dataBase);
            LoadObjects(roomReader);
            //Sort(); //todo???
        }

        private void LoadStatic(StreamReader roomReader, GODataBase dataBase)
        {
            int[] parsedLine;
            for (int y = 0; y < 32; y++)
            {
                parsedLine = StringArrayToInt(roomReader.ReadLine().Split(","));
                for (int x = 0; x < 32; x++)
                {
                    SetStaticAt(x, y, dataBase.CloneByID(parsedLine[x]));
                    GetStaticAt(x, y).SetPosition(x, y);
                }
            }
        }

        private void LoadObjects(StreamReader roomReader)
        {
            string currentLine;

            while ((currentLine = roomReader.ReadLine()) != "eof")
            {
                // Определяет тип загружаемого объекта, и выполняет его загрузку
                switch (currentLine)
                {
                    case "NPC": AddNPC(LoadObject<NPC>(roomReader)); break;
                    case "TRIGGER": AddTrigger(LoadObject<Trigger>(roomReader)); break;

                    default: break;
                }
            }
        }

        private T LoadObject<T>(StreamReader objectReader) where T : GameObject, new()
        {
            T newInstance = new T();
            newInstance.Load(objectReader, GetParentWorld());
            return newInstance;
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

        public World GetParentWorld()
        {
            return parentWorld;
        }

        public void SetParentWorld(World parentWorld)
        {
            this.parentWorld = parentWorld;
        }

        public bool IsAnyTriggerAt(int x, int y)
        {
            return GetTriggerAt(x, y) != null;
        }

        public bool IsAnyTriggerAt(Point position)
        {
            return GetTriggerAt(position.X, position.Y) != null;
        }

        public Trigger GetTriggerAt(int x, int y)
        {
            foreach (Trigger instant in listTriggers)
            {
                if (instant.IsObjectAt(x, y)) return instant;
            }

            return null;
        }

        public Trigger GetTriggerAt(Point pos)
        {
            return GetTriggerAt(pos.X, pos.Y);
        }

        public void SetStaticAt(int x, int y, Static obj)
        {
            mapStatic[y, x] = obj;
        }

        public Static GetStaticAt(int x, int y)
        {
            return mapStatic[y, x];
        }

        public Static GetStaticAt(Point position)
        {
            return mapStatic[position.Y, position.X];
        }

        public void AddTrigger(Trigger newTrigger)
        {
            newTrigger.SetCurrentRoom(this);
            listTriggers.Add(newTrigger);
        }

        public void AddNPC(NPC npc)
        {
            npc.SetCurrentRoom(this);
            listNPC.Add(npc);
        }

        public bool IsAnyNPCAt(int x, int y)
        {
            foreach (NPC npc in listNPC)
            {
                if (npc.IsObjectAt(x, y))
                {
                    return true;
                }
            }

            return false;
        }

        public NPC GetNPCAt(int x, int y)
        {
            if (IsAnyNPCAt(x, y))
            {
                foreach (NPC npc in listNPC)
                {
                    if (npc.IsObjectAt(x, y))
                    {
                        return npc;
                    }
                }
            }

            return null;
        }
    }
}
