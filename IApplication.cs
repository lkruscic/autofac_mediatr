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
    public interface IMyApplication
    {
        void RunPing(string key);
        void RunPong(string key);
    }

    public class MyApplication : IMyApplication
    {
        private readonly IMediator _mediator;
        private readonly IDependencyResolver _dependencyResolver;

        public MyApplication(IDependencyResolver dependencyResolver)
        {
           _dependencyResolver = dependencyResolver ?? IoC.DependencyResolver.Instance;
           _mediator = _dependencyResolver.Resolve<IMediator>();
        }

        public void RunPing(string key)
        {
            var o = _dependencyResolver.ResolveNamed<IPing>(key);
            o.Message = $"Request with the key: {key} ";


            var defBColor = Console.BackgroundColor;
            var defFColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;

            _mediator.Send(o);

            Console.BackgroundColor = defBColor;
            Console.ForegroundColor = defFColor;

        }


        public void RunPong(string key)
        {
            //var o = (IBong<IReturnBong>)c.ResolveKeyed<IRequest>(key);
            var o = _dependencyResolver.ResolveNamed<IPong>(key);

            var defBColor = Console.BackgroundColor;
            var defFColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;

            o.Message =  $"Request message from {key}";
            var r = _mediator.Send(o).Result;

            Console.WriteLine("----");
            Console.WriteLine(r);
            Console.WriteLine("-----");

            Console.BackgroundColor = defBColor;
            Console.ForegroundColor = defFColor;

        }
    }
}
