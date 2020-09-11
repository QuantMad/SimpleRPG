using System;
using System.Text.RegularExpressions;

namespace SimpleRPG.Core
{
    class DrawingBuffer
    {
        public const int WIDTH = 64;
        public const int HEIGHT = 32;

        string[,] screenBuffer = new string[HEIGHT, WIDTH];

        public void Clear()
        {
            for (int y = 0; y < screenBuffer.GetLength(0); y++)
            {
                for (int x = 0; x < screenBuffer.GetLength(1); x++)
                {
                    screenBuffer[y, x] = "__";
                }
            }
        }

        public string Render()
        {
            string outLine = "";

            for (int y = 0; y < screenBuffer.GetLength(0); y++)
            {
                for (int x = 0; x < screenBuffer.GetLength(1); x++)
                {
                    outLine += screenBuffer[y, x];
                }
                outLine += "\n";
            }

            return outLine;
        }

        public void DrawTextAt(string text, int x, int y)
        {
            string _text = text + (text.Length % 2 != 0 ? " " : "");

            string[] line = new string[_text.Length / 2];

            Regex regular = new Regex(".{0,2}", RegexOptions.Singleline);
            MatchCollection matches = regular.Matches(_text);

            int i = 0;
            foreach (Match match in matches)
            {
                if (i < line.Length) // Костыль. Переработать
                {
                    line[i++] = match.Value;
                }
            }

            DrawAreaAt(line, x, y);
        }

        public void DrawElementAt(string symbol, int x, int y)
        {
            CheckCoordinatesAvalability(x, y);

            screenBuffer[y, x] = symbol;
        }

        public void DrawAreaAt(string[,] array, int x, int y)
        {
            CheckCoordinatesAvalability(x, y);
            CheckAreaAvalability(array, x, y);

            for (int _y = 0; _y < array.GetLength(0); _y++)
            {
                for (int _x = 0; _x < array.GetLength(1); _x++)
                {
                    screenBuffer[y + _y, x + _x] = array[_y, _x];
                }
            }
        }

        public void DrawAreaAt(string[] array, int x, int y)
        {
            CheckCoordinatesAvalability(x, y);
            CheckAreaAvalability(array, x, y);

            for (int _x = 0; _x < array.Length; _x++)
            {
                screenBuffer[y, x + _x] = array[_x];
            }

        }

        private void CheckCoordinatesAvalability(int x, int y)
        {
            if (x > screenBuffer.GetLength(1) ||
                y > screenBuffer.GetLength(0) ||
                x < 0 || y < 0)
            {
                throw new IndexOutOfRangeException("Area coordinates out of screen buffer limits.");
            }
        }

        private void CheckAreaAvalability(string[,] area, int x, int y)
        {
            if (x + area.GetLength(1) > screenBuffer.GetLength(1) ||
                y + area.GetLength(0) > screenBuffer.GetLength(0))
            {
                throw new IndexOutOfRangeException("The area is outside the screen buffer.");
            }
        }

        private void CheckAreaAvalability(string[] area, int x, int y)
        {
            if (x + area.Length > screenBuffer.GetLength(1))
            {
                throw new IndexOutOfRangeException("The area is outside the screen buffer.");
            }
        }
    }
}
