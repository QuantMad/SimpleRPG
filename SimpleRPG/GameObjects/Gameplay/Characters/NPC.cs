namespace ConsoleApp1.GameObjects.Characters
{
    class NPC : Character
    {
        const string DEFAULT_GRAPHICS = "☺ ";

        public NPC()
        {
            SetGraphics(DEFAULT_GRAPHICS);
        }

        //private Inventory inventory; // TODO
        //private Equipment equipment; // TODO
        //private string relation; // TODO
        //private List<Quest> vacantQuests; // TODO
    }
}
