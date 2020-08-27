namespace ConsoleApp1.Core
{
    public class Point
    {
        public int X = 0, Y = 0;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }
    }
}
