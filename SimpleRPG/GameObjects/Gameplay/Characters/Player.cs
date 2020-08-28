using System;

namespace SimpleRPG.GameObjects.Characters
{
    class Player : Character
    {
        public Player()
        {
            SetGraphics("██");
        }

        public override void Step(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.D: MoveRelative(0, 1); break;
                case ConsoleKey.A: MoveRelative(0, -1); break;
                case ConsoleKey.S: MoveRelative(1, 0); break;
                case ConsoleKey.W: MoveRelative(-1, 0); break;
                case ConsoleKey.Enter:
                    if (GetCurrentRoom().IsAnyTriggerAt(GetPosition()))
                    {
                        GetCurrentRoom().GetTriggerAt(GetPosition()).Activate(this);
                    }
                    break;
            }
        }

        //private Inventory inventory; // TODO
        //private Equipment equipment; // TODO
        //private string relation; // TODO
        //private List<Quest> ActiveQuests; // TODO
    }
}
