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
            dataBase = new GODataBase("GODataBase.db");
            //dataBase.Connect();

            //dataBase.GetObjectByID(2);

            /*ConsoleKey input;

            while ((input = Console.ReadKey().Key) != ConsoleKey.Q)
            {

            }*/
        }
    }
}
