using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using MediatR;
using MediatR.Pipeline;
using Module = Autofac.Module;

namespace autofac_mediatR.IoC
{
    public class AppServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterMediator(builder);
            RegisterPingRequests(builder);

            builder.RegisterType<Application>().As<IApplication>();
            //builder.RegisterType<IContainer>().As<IContainer>();
        }

        private static void RegisterMediator(ContainerBuilder builder)
        {
            builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

            builder
               .RegisterType(typeof(PingAHandler))
               .As(typeof(RequestHandler<PingA>));
            

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // builder.RegisterGeneric(typeof(LoggingBehavior<>)).As(typeof(IRequestPreProcessor<>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            // builder.RegisterGeneric(typeof(ExceptionHandler<,>)).As(typeof(IRequestExceptionHandler<,,>));
            builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            // builder.Register(c => new PingAHandler().As<RequestHandler)

        }

        private static void RegisterPingRequests(ContainerBuilder builder)
        {
            // builder.RegisterType<PingA>().Keyed<IPing>("PingA");
            // builder.RegisterType<PingB>().Keyed<IRequest>("PingB");
            // builder.RegisterType<PingC>().Keyed<IRequest>("PingC");

            builder
               .RegisterType<PingA>()
               .As<IRequest>()
               .Keyed<IRequest>("PingA");
               //.WithParameter
               //(
               //   new TypedParameter(typeof(Tuple<string, string>), "t")
               //);



            //.WithParameter(
            //   new ResolvedParameter(
            //      (pi, ctx) => pi.ParameterType == typeof(Tuple<string, string>),
            //      (pi, ctx) => ctx.ResolveKeyed<Tuple<string, string>>("PingA"))
            //   )
            //.Keyed<IRequest>("PingA");

            //builder.RegisterType<PingB>()
            //.WithParameter(
            //   new ResolvedParameter(
            //      (pi, ctx) => pi.ParameterType == typeof(Tuple<string, string>),
            //      (pi, ctx) => ctx.ResolveKeyed<IPing>("PingB")));            

            //builder.RegisterType<PingC>()
            //.WithParameter(
            //   new ResolvedParameter(
            //      (pi, ctx) => pi.ParameterType == typeof(Tuple<string, string>),
            //      (pi, ctx) => ctx.ResolveKeyed<IPing>("PingC")));

        }



}
}
