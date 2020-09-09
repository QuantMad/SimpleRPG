using System;

namespace SimpleRPG.GameObjects.Characters
{
    /**
     * Этот класс содержит общее представление объекта-персонажа. 
     * Необходим для достижения должного уровня абстракции при работе с игровыми и неигровыми персонажами
     **/
    class Character : GameObject
    {
        // TODO: Разработать систему ролей персонажей (торговцы, разбойники, и т.д.). 
        //       Сделать доавление новых классов простым в реализации
        private string role;

        public override void Load(System.IO.StreamReader objectReader, World currentWorld)
        {
            base.Load(objectReader, currentWorld);

            string currentLine, key, val;

            while ((currentLine = objectReader.ReadLine()) != END)
            {
                if (currentLine.Length > 0)
                {
                    key = currentLine.Split('=')[0];
                    val = currentLine.Split('=')[1];

                    switch (key)
                    {
                        case "Role": SetRole(val); break;

                        default: break;
                    }
                }
            }
        }

        // Нужно ли?
        public virtual void Step(ConsoleKey input)
        {

        }

        public void SetRole(string Role)
        {
            this.role = Role;
        }

        public string GetRole()
        {
            return role;
        }

        public void MoveRelative(int vertical, int horizontal)
        {
            // Переработать
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

            newInstant.SetRole(role);

            return newInstant;
        }

        protected override GameObject CreateCloneBase()
        {
            return new Character();
        }
    }
}
