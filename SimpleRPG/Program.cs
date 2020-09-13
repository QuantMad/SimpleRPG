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


            while ((input = Console.ReadKey().Key) != ConsoleKey.Q)
            {
                mainWorld.player.Step(input);

                mainBuffer.Clear();
                mainBuffer.DrawAreaAt(mainWorld.BuildRoomImage(mainWorld.player.GetCurrentRoom()), 0, 0);

                mainBuffer.DrawTextAt("Name: " + mainWorld.player.GetName(), 35, 2);
                mainBuffer.DrawTextAt("Position X: " + mainWorld.player.GetPosition().X, 35, 3);
                mainBuffer.DrawTextAt("Position Y: " + mainWorld.player.GetPosition().Y, 35, 4);

                Console.Clear();
                Console.WriteLine(mainBuffer.Render()); ;
            }
        }
    }
}
