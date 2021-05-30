﻿using System;
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


            var mediatrOpenTypes = new[]
            {
               typeof(IRequestHandler<,>),
               typeof(IRequestExceptionHandler<,,>),
               typeof(IRequestExceptionAction<,>),
               typeof(INotificationHandler<>),
               typeof(RequestHandler<>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(typeof(IPing).GetTypeInfo().Assembly)
                .AsClosedTypesOf(mediatrOpenType)
                // when having a single class implementing several handler types
                // this call will cause a handler to be called twice
                // in general you should try to avoid having a class implementing for instance `IRequestHandler<,>` and `INotificationHandler<>`
                // the other option would be to remove this call
                // see also https://github.com/jbogard/MediatR/issues/462
                .AsImplementedInterfaces();
            }

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

        }

        private static void RegisterPingRequests(ContainerBuilder builder)
        {
            builder
                .RegisterType<PingA>()
                .As<IRequest>()
                .Keyed<IRequest>("PingA");

            builder
               .RegisterType<PingB>()
               .As<IRequest>()
               .Keyed<IRequest>("PingB");

            builder
               .RegisterType<PingC>()
               .As<IRequest>()
               .Keyed<IRequest>("PingC");

        }

    }
}
