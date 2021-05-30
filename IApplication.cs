using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using autofac_mediatR.IoC;
using MediatR;

namespace autofac_mediatR
{
    public interface IApplication
    {
        void Run(IContainer c, string key);
    }

    public class Application : IApplication
    {
        protected IMediator Mediator;
        public Application(IMediator mediator)
        {
            Mediator = mediator;
        }

        public void Run(IContainer c, string key)
        {
            var o = (IPing)c.ResolveKeyed<IRequest>(key);
            o.StringTuple = new Tuple<string, string>($"item 1: {key}", $"item 2: {key}");
            var defBColor = Console.BackgroundColor;
            var defFColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;

            Mediator.Send(o);

            Console.BackgroundColor = defBColor;
            Console.ForegroundColor = defFColor;

        }
    }
}
