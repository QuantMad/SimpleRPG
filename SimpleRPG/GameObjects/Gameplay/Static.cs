namespace SimpleRPG.GameObjects.Core
{
    class Static : GameObject
    {
        public bool IsObstacle
        {
            get { return IsObstacle; }
            set { IsObstacle = value; }
        }

        public new Static Clone()
        {
            Static newInstance = (Static)base.Clone();

            newInstance.IsObstacle = IsObstacle;

            return newInstance;
        }

        protected override GameObject CreateCloneBase()
        {
            return new Static();
        }
    }
}
