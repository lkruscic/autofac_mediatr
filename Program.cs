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
    class Program
    {
        static void Main(string[] args)
        {
            // create an autofac container builder
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AppServiceModule>();
            var c = containerBuilder.Build();
            var app = c.Resolve<IApplication>();
            app.Run(c);

        }
    }
}
