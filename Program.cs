using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using autofac_mediatR.IoC;
using MediatR;

namespace autofac_mediatR
{
    class Program
    {
        static void Main(string[] args)
        {
            // create an autofac container builder
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AppServiceModule>();
            var c = containerBuilder.Build();
            var app = c.Resolve<IApplication>();

            ConsoleKey response;
            do
            {
                PrintValidChoices();
                response = Console.ReadKey(false).Key;
                Console.WriteLine("=---------------------------");
                Console.WriteLine($"Selection: {response}");

                switch (response)
                {
                    case ConsoleKey.D1:
                        app.Run(c, "PingA");
                        break;
                    case ConsoleKey.D2:
                        app.Run(c, "PingB");
                        break;
                    case ConsoleKey.D3:
                        app.Run(c, "PingC");
                        break;
                }
            } while (response != ConsoleKey.D4);

        }


        static void PrintValidChoices()
        {
            Console.WriteLine("");
            Console.WriteLine("==========================");
            Console.WriteLine("1 => 'PingA'");
            Console.WriteLine("2 => 'PingB'");
            Console.WriteLine("3 => 'PingC'");
            Console.WriteLine("4 => 'Exit'");
            Console.WriteLine("Please make a selection!");
            Console.WriteLine("==========================");
            Console.WriteLine("");
        }
    }
}
