namespace ConsoleApp1.GameObjects.Core
{
    class Static : GameObject
    {
        private bool isObstacle;

        public bool IsObstacle()
        {
            return isObstacle;
        }

        public void SetIsObstacle(bool isObstacle)
        {
            this.isObstacle = isObstacle;
        }

        public void SetIsObstacle(string rawVal)
        {
            SetIsObstacle(bool.Parse(rawVal));
        }

        public new Static Clone()
        {
            Static newInstance = (Static)base.Clone();

            newInstance.SetIsObstacle(IsObstacle());

            return newInstance;
        }

        protected override GameObject CreateCloneBase()
        {
            return new Static();
        }
    }
}
