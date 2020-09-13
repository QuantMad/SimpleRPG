using SimpleRPG.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SimpleRPG
{
    class GameObject : IDrawable, IComparer<int>
    {
        // Константа определяющая конец описния объекта
        protected const string END = "end";

        protected const int DRAWING_PRIORITY_STATIC = 0;
        protected const int DRAWING_PRIORITY_ITEM = 1;
        protected const int DRAWING_PRIORITY_TRIGGER = 2;
        protected const int DRAWING_PRIORITY_CHARACTER = 3;

        /**
         * Базовые поля, пресущие любому игрвому объекту
         **/
        private int ID = 0;
        private string name;
        private string graphics;
        private Point position = new Point(0, 0); // Сделать readonly?
        protected int drawingPriority = -1;

        private Room currentRoom;

        /**
         * Базовый метод загрузки объекта их птока objectReader
         * TODO: currentWorld - костыль. Исправить. 
         **/
        public virtual void Load(StreamReader objectReader, World currentWorld)
        {
            string currentLine, key, val;
            int x, y;

            for (int i = 0; i < 4; i++)
            {
                currentLine = objectReader.ReadLine();

                if (currentLine.Length > 0)
                {
                    key = currentLine.Split('=')[0];
                    val = currentLine.Split('=')[1];

                    x = val.Contains(':') ? int.Parse(val.Split(':')[0]) : 0;
                    y = val.Contains(':') ? int.Parse(val.Split(':')[1]) : 0;

                    switch (key)
                    {
                        case "ID": SetID(int.Parse(val)); break;
                        case "name": SetName(val); break;
                        case "graphics": SetGraphics(val); break;
                        case "position": SetPosition(x, y); break;

                        default: break;
                    }
                }
            }
        }

        public bool IsObjectAt(int x, int y)
        {
            return position.X == x && position.Y == y;
        }

        public void SetID(int ID)
        {
            this.ID = ID;
        }

        public int GetID()
        {
            return ID;
        }

        public void SetGraphics(String graphics)
        {
            this.graphics = graphics;
        }

        public string GetGraphics()
        {
            return graphics;
        }

        public void SetPosition(Point position)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
        }

        public void SetPosition(int X, int Y)
        {
            position.X = X;
            position.Y = Y;
        }

        public Point GetPosition()
        {
            return position;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public World GetWorld()
        {
            return currentRoom.GetParentWorld();
        }

        public void SetCurrentRoom(Room currentRoom)
        {
            this.currentRoom = currentRoom;
        }

        public Room GetCurrentRoom()
        {
            return currentRoom;
        }


        public virtual GameObject Clone()
        {
            GameObject newObject = CreateCloneBase();

            newObject.SetID(ID);
            newObject.SetGraphics(graphics);
            newObject.SetPosition(position.X, position.Y);
            newObject.SetCurrentRoom(currentRoom);
            newObject.SetName(name);

            return newObject;
        }

        protected virtual GameObject CreateCloneBase()
        {
            return new GameObject();
        }

        public int GetDrawingPriority()
        {
            return drawingPriority;
        }

        public int Compare([AllowNull] int x, [AllowNull] int y)
        {
            return x > y ? x : y;
        }
    }
}
