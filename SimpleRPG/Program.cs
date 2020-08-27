using ConsoleApp1.GameObjects.Characters;
using System;
using System.Text;

namespace ConsoleApp1
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

                Console.Clear();
                Console.WriteLine(mainWorld.RenderRoom(mainWorld.player.GetCurrentRoom()));
                Console.WriteLine(mainWorld.player.GetPosition().X + " | " + mainWorld.player.GetPosition().Y);
            }
        }
    }
}
