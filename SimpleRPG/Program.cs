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
            dataBase = new GODataBase();
            dataBase.Load();

            mainWorld = new World("world1", dataBase);
            mainWorld.Load();

            mainWorld.player = new Player();
            mainWorld.player.SetCurrentRoom(mainWorld.GetRoomAt(0, 0));
            mainWorld.player.SetPosition(30, 11);

            ConsoleKey input;
            string outline;

            while ((input = Console.ReadKey().Key) != ConsoleKey.Q)
            {
                mainWorld.player.Step(input);
                outline = mainWorld.RenderRoom(mainWorld.player.GetCurrentRoom());

                Console.Clear();
                Console.WriteLine(outline);

                //выводит координаты игрока
                //Console.WriteLine(mainWorld.player.GetPosition().X + " | " + mainWorld.player.GetPosition().Y);
            }
        }
    }
}
