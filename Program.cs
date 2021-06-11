using autofac_mediatR.IoC;
using System;
using System.Threading.Tasks;

namespace autofac_mediatR
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var app = DependencyResolver.Instance.Resolve<IMyApplication>();
            PrintValidChoices();
            ConsoleKey response;
            do
            {
                response = Console.ReadKey(false).Key;
                Console.WriteLine("=---------------------------");
                Console.WriteLine($"Selection: {response}");

                switch (response)
                {
                    case ConsoleKey.D1:
                        app.RunPing("PingA");
                        break;
                    case ConsoleKey.D2:
                        app.RunPing("PingB");
                        break;
                    case ConsoleKey.D3:
                        app.RunPing("PingC");
                        break;
                    case ConsoleKey.D4:
                        app.RunPong("PongA");
                        break;                    
                    case ConsoleKey.D5:
                        app.RunPong("PongB");
                        break;                    
                    case ConsoleKey.D6:
                        app.RunPong("PongC");
                        break;
                    case ConsoleKey.I:
                        PrintValidChoices();
                        break;
                }
            } while (response != ConsoleKey.D9);
        }


        static void PrintValidChoices()
        {
            Console.WriteLine("");
            Console.WriteLine("==========================");
            Console.WriteLine("i => 'Instruction'");
            Console.WriteLine("1 => 'PingA'");
            Console.WriteLine("2 => 'PingB'");
            Console.WriteLine("3 => 'PingC'");
            Console.WriteLine("4 => 'PongA'");
            Console.WriteLine("5 => 'PongB'");
            Console.WriteLine("6 => 'PongC'");
            Console.WriteLine("9 => 'Exit'");
            Console.WriteLine("Please make a selection!");
            Console.WriteLine("==========================");
            Console.WriteLine("");
        }
    }
}
