using System;
using System.Text;

namespace SimpleRPG.Core
{
    class DrawingBuffer
    {
        public const int WIDTH = 64;
        public const int HEIGHT = 32;

        private readonly StringBuilder[,] firstBuffer = new StringBuilder[HEIGHT, WIDTH];
        private readonly StringBuilder[,] secondBuffer = new StringBuilder[HEIGHT, WIDTH];
        private StringBuilder[,] currentBuffer;

        public DrawingBuffer()
        {
            Console.CursorVisible = false;
            currentBuffer = firstBuffer;
            for (int y = 0; y < firstBuffer.GetLength(0); y++)
            {
                for (int x = 0; x < firstBuffer.GetLength(1); x++)
                {
                    firstBuffer[y, x] = new StringBuilder();
                }
            }

            for (int y = 0; y < secondBuffer.GetLength(0); y++)
            {
                for (int x = 0; x < secondBuffer.GetLength(1); x++)
                {
                    secondBuffer[y, x] = new StringBuilder();
                }
            }

            Clear(firstBuffer);
            Clear(secondBuffer);
        }

        public void Clear(StringBuilder[,] buffer)
        {
            for (int y = 0; y < buffer.GetLength(0); y++)
            {
                for (int x = 0; x < buffer.GetLength(1); x++)
                {
                    buffer[y, x].Clear();
                    buffer[y, x].Append("__");
                }
            }
        }

        public void Clear()
        {
            Clear(currentBuffer);
        }

        public void Render()
        {
            for (int y = 0; y < currentBuffer.GetLength(0); y++)
            {
                for (int x = 0; x < currentBuffer.GetLength(1) * 2; x += 2)
                {
                    if (firstBuffer[y, x / 2].ToString() != secondBuffer[y, x / 2].ToString())
                    {
                        WriteAt(currentBuffer[y, x / 2].ToString()[0].ToString(), x, y);
                        WriteAt(currentBuffer[y, x / 2].ToString()[1].ToString(), x + 1, y);
                    }
                }
            }

            currentBuffer = currentBuffer == firstBuffer ? secondBuffer : firstBuffer;
        }

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s, Console.CursorVisible);
                Console.SetCursorPosition(64, 32);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DrawTextAt(string text, int x, int y)
        {
            string _text = text + (text.Length % 2 != 0 ? " " : "");

            string[] line = new string[_text.Length / 2];

            for (int i = 0; i < _text.Length; i++)
            {
                if (i % 2 != 0)
                {
                    line[i / 2] = _text[i - 1].ToString() + _text[i].ToString();
                }
            }

            DrawAreaAt(line, x, y);
        }

        public void DrawElementAt(string element, int x, int y)
        {
            CheckCoordinatesAvalability(x, y);

            currentBuffer[y, x].Clear();
            currentBuffer[y, x].Append(element);
        }

        public void DrawAreaAt(string[,] area, int x, int y)
        {
            CheckCoordinatesAvalability(x, y);
            CheckAreaAvalability(area, x, y);

            for (int _y = 0; _y < area.GetLength(0); _y++)
            {
                for (int _x = 0; _x < area.GetLength(1); _x++)
                {
                    DrawElementAt(area[_y, _x], x + _x, y + _y);
                }
            }
        }

        public void DrawAreaAt(string[] area, int x, int y)
        {
            CheckCoordinatesAvalability(x, y);
            CheckAreaAvalability(area, x, y);

            for (int _x = 0; _x < area.Length; _x++)
            {
                DrawElementAt(area[_x], x + _x, y);
            }

        }

        private void CheckCoordinatesAvalability(int x, int y)
        {
            if (x > currentBuffer.GetLength(1) ||
                y > currentBuffer.GetLength(0) ||
                x < 0 || y < 0)
            {
                throw new IndexOutOfRangeException("Area coordinates out of screen buffer limits.");
            }
        }

        private void CheckAreaAvalability(string[,] area, int x, int y)
        {
            if (x + area.GetLength(1) > currentBuffer.GetLength(1) ||
                y + area.GetLength(0) > currentBuffer.GetLength(0))
            {
                throw new IndexOutOfRangeException("The area is outside the screen buffer.");
            }
        }

        private void CheckAreaAvalability(string[] area, int x, int y)
        {
            if (x + area.Length > currentBuffer.GetLength(1))
            {
                throw new IndexOutOfRangeException("The area is outside the screen buffer.");
            }
        }
    }
}
