using System;

namespace ConsoleApp1.GameObjects.Characters
{
    class Character : GameObject
    {
        const string TYPE_TRADER = "Trader";
        const string TYPE_CITYZEN = "Cityzen";
        const string TYPE_WARRIOR = "Warrior";
        const string TYPE_ROBBER = "Robber";
        const string TYPE_GUARD = "Guard";

        private string Role;

        public virtual void Step(ConsoleKey input)
        {

        }

        public void SetRole(string Role)
        {
            this.Role = Role;
        }

        public string GetRole()
        {
            return Role;
        }

        public void MoveRelative(int vertical, int horizontal)
        {

            if (GetPosition().X + horizontal < 32 && GetPosition().X + horizontal > -1 &&
                GetPosition().Y + vertical < 32 && GetPosition().Y + vertical > -1 &&
                GetCurrentRoom().GetStaticAt(GetPosition().X + horizontal, GetPosition().Y + vertical).IsObstacle())
            {
                SetPosition(GetPosition().X + horizontal, GetPosition().Y + vertical);
            }
        }

        public new Character Clone()
        {
            Character newInstant = (Character)base.Clone();

            newInstant.SetRole(Role);

            return newInstant;
        }

        protected override GameObject CreateCloneBase()
        {
            return new Character();
        }
    }
}
