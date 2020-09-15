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
        public string Role
        {
            get { return Role; }
            set { Role = value; }
        }

        // Нужно ли?
        public virtual void Step(ConsoleKey input)
        {

        }

        public void MoveRelative(int vertical, int horizontal)
        {
            // Переработать
            /*if (GetPosition().X + horizontal < 32 && GetPosition().X + horizontal > -1 &&
                GetPosition().Y + vertical < 32 && GetPosition().Y + vertical > -1 &&
                GetCurrentRoom().GetStaticAt(GetPosition().X + horizontal, GetPosition().Y + vertical).IsObstacle)
            {
                SetPosition(GetPosition().X + horizontal, GetPosition().Y + vertical);
            }*/
        }

        public new Character Clone()
        {
            Character newInstant = (Character)base.Clone();

            newInstant.Role = Role;

            return newInstant;
        }

        protected override GameObject CreateCloneBase()
        {
            return new Character();
        }
    }
}
