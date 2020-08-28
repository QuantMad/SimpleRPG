using SimpleRPG.Core;
using SimpleRPG.GameObjects.Characters;
using System.IO;

namespace SimpleRPG.GameObjects.Core
{
    class Trigger : GameObject
    {
        private Room remoteRoom;
        private Point remotePosition = new Point(0, 0);

        public void Load(StreamReader objectReader, World currentWorld)
        {
            base.Load(objectReader);

            string currentLine, key, val;
            int x, y;

            while ((currentLine = objectReader.ReadLine()) != END)
            {
                if (currentLine.Length > 0)
                {
                    key = currentLine.Split("=")[0];
                    val = currentLine.Split("=")[1];

                    x = val.Contains(':') ? int.Parse(val.Split(':')[0]) : 0;
                    y = val.Contains(':') ? int.Parse(val.Split(':')[1]) : 0;

                    switch (key)
                    {
                        case "remoteRoom": SetRemoteRoom(currentWorld.GetRoomAt(x, y)); break;
                        case "remotePosition": SetRemotePosition(x, y); break;
                    }
                }
            }
        }

        public void SetRemoteRoom(Room remoteRoom)
        {
            this.remoteRoom = remoteRoom;
        }

        public void SetRemotePosition(int x, int y)
        {
            remotePosition.X = x;
            remotePosition.Y = y;
        }

        public void Activate(Player player)
        {
            player.SetPosition(remotePosition.X, remotePosition.Y);
            player.SetCurrentRoom(remoteRoom);
        }
    }
}
