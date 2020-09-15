using System.Data.SQLite;

namespace SimpleRPG.GameObjects.Core
{
    class Static : GameObject
    {
        public bool IsObstacle
        {
            get; protected set;
        }

        public override void Load(SQLiteDataReader dataReader)
        {
            base.Load(dataReader);

            IsObstacle = bool.Parse(dataReader.GetString(3));
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
