using ConsoleApp1.Core;
using ConsoleApp1.GameObjects.Characters;

namespace ConsoleApp1.GameObjects.Core
{
    class Trigger : GameObject // TODO: Wrapper
    {
        private Room remoteRoom;
        private Point remotePosition = new Point(0, 0);

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
