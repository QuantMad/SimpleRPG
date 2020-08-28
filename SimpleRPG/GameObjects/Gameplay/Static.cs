using System.IO;

namespace SimpleRPG.GameObjects.Core
{
    class Static : GameObject
    {
        private bool isObstacle;

        public override void Load(StreamReader objectReader, World currentWorld)
        {
            base.Load(objectReader, currentWorld);
            string currentLine, key, val;

            while ((currentLine = objectReader.ReadLine()) != END)
            {
                key = currentLine.Split('=')[0];
                val = currentLine.Split('=')[1];

                switch (key)
                {
                    case "isObstacle": SetIsObstacle(val); break;

                    default: break;
                }
            }
        }

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
