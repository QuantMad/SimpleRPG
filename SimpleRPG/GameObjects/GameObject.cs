using ConsoleApp1.Core;
using System;

namespace ConsoleApp1
{
    class GameObject
    {
        private int ID = 0;
        private string graphics;
        private Point position = new Point(0, 0);
        private Room currentRoom;
        private string name;

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
    }
}
