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
        void Run(IContainer c);
    }

    public class Application : IApplication
    {
        protected IMediator Mediator;
        public Application(IMediator mediator)
        {
            Mediator = mediator;
        }

        public void Run(IContainer c)
        {
            string key = "PingA";
            var o = (PingA)c.ResolveKeyed<IRequest>(key);
            o.StringTuple = new Tuple<string, string>("ping aaa1", "  ping aaa2");
            Mediator.Send(o);
        }



    }
}
