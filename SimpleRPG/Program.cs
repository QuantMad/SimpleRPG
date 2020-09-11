using SimpleRPG.Core;
using SimpleRPG.GameObjects.Characters;
using System;
using System.Text;

namespace SimpleRPG
{
    class Program
    {
        private GODataBase dataBase;
        private World mainWorld;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Program game = new Program();
            game.Run();
        }

        public void Run()
        {
            DrawingBuffer mainBuffer = new DrawingBuffer();

            dataBase = new GODataBase();
            dataBase.Load();

            mainWorld = new World("world1", dataBase);
            mainWorld.Load();

            mainWorld.player = new Player();
            mainWorld.player.SetCurrentRoom(mainWorld.GetRoomAt(0, 0));
            mainWorld.player.SetPosition(30, 11);

            ConsoleKey input;
            //string outline;

            while ((input = Console.ReadKey().Key) != ConsoleKey.Q)
            {
                mainBuffer.Clear();
                mainWorld.player.Step(input);

                //outline = mainWorld.RenderRoom(mainWorld.player.GetCurrentRoom());
                mainBuffer.DrawElementAt("XX", 2, 2);
                mainBuffer.DrawAreaAt(mainWorld.BuildRoomImage(mainWorld.player.GetCurrentRoom()), 0, 0);
                mainBuffer.DrawTextAt("test", 34, 2);

                Console.Clear();
                Console.WriteLine(mainBuffer.Render());

                //выводит координаты игрока
                //Console.WriteLine(mainWorld.player.GetPosition().X + " | " + mainWorld.player.GetPosition().Y);
            }
        }
    }
}
