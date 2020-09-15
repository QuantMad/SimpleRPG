using SimpleRPG.Core;
using SimpleRPG.GameObjects.Characters;

namespace SimpleRPG.GameObjects.Core
{
    class Trigger : GameObject
    {
        private Room remoteRoom;
        private readonly Point remotePosition = new Point(0, 0);

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
