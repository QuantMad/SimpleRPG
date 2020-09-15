using SimpleRPG.Core;
using System;
using System.Data.SQLite;

namespace SimpleRPG
{
    class GameObject
    {
        private Room currentRoom;

        public readonly Point position = new Point(0, 0);

        public int ID { get; private set; }

        public string Name { get; protected set; }

        public Chank Graphics { get; protected set; }

        public GameObject()
        {
            Graphics = new Chank("XX");
        }

        public virtual void Load(SQLiteDataReader dataReader)
        {
            ID = Convert.ToInt32(dataReader.GetInt64(0));
            Name = dataReader.GetString(1);
            string s = dataReader.GetString(2);
            Graphics.Set(dataReader.GetString(2));
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
            newObject.Graphics = Graphics;
            newObject.SetPosition(position.X, position.Y);
            newObject.SetCurrentRoom(currentRoom);
            newObject.Name = Name;

            return newObject;
        }

        protected virtual GameObject CreateCloneBase()
        {
            return new GameObject();
        }
    }
}
