namespace SimpleRPG.Core
{
    // Этот класс является простым контейнером для двух координат целочисленного типа. 
    // Служит для удобства передачи данных между объектами
    public class Point
    {
        // Переменные координат
        public int X, Y;

        // Конструктор без определения координат
        public Point()
        {
            X = 0;
            Y = 0;
        }

        // Конструктор с определением координат
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Создаёт копию объекта
        public Point Clone()
        {
            return new Point(X, Y);
        }
    }
}
