using System;
using System.Text;

namespace SimpleRPG.Core
{
    class DrawingBuffer
    {
        public const int WIDTH = 64;
        public const int HEIGHT = 32;

        private readonly Chank[,] screenBuffer = new Chank[HEIGHT, WIDTH];
        private readonly StringBuilder outLine = new StringBuilder();

        public DrawingBuffer()
        {
            outLine.Capacity = screenBuffer.GetLength(0) * screenBuffer.GetLength(1);

            for (int y = 0; y < screenBuffer.GetLength(0); y++)
            {
                for (int x = 0; x < screenBuffer.GetLength(1); x++)
                {
                    screenBuffer[y, x] = new Chank();
                }
            }
        }

        public void Clear()
        {

        }

        public void Render()
        {
            for (int y = 0; y < 32; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    outLine.Append(screenBuffer[y, x]);
                }
                outLine.Append('\n');
            }

            Console.Write(outLine);
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
            screenBuffer[y, x].Set(element);
        }

        public void DrawAreaAt(string[,] area, int x, int y)
        {
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
            for (int _x = 0; _x < area.Length; _x++)
            {
                DrawElementAt(area[_x], x + _x, y);
            }

        }
    }
}
