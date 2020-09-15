using SimpleRPG.Core;

namespace SimpleRPG.GameObjects.Characters
{
    class NPC : Character
    {
        private static readonly Chank DEFAULT_GRAPHICS = new Chank("☺ ");

        public NPC()
        {
            Graphics = DEFAULT_GRAPHICS;
        }

        //private Inventory inventory; // TODO
        //private Equipment equipment; // TODO
        //private string relation; // TODO
        //private List<Quest> vacantQuests; // TODO
    }
}
