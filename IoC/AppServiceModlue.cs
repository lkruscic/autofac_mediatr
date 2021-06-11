using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Module = Autofac.Module;

namespace autofac_mediatR.IoC
{
    public class AppServiceModule : Module
    {
        private static IEnumerable<Type> _assemblyTypes;
        private static IEnumerable<Type> AssemblyTypes
        {
            get
            {
                if (_assemblyTypes != null)
                    return _assemblyTypes;

                List<Type> result = new List<Type>();
                var assemblyFiles = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                .GetFiles()
                .Where(x => x.Name.StartsWith("autofac_mediat") && (x.Name.EndsWith("exe") || x.Name.EndsWith("dll")));
                foreach (var assemblyFile in assemblyFiles)
                {
                    var assembly = Assembly.LoadFile(assemblyFile.FullName);
                    result.AddRange(assembly.GetTypes().ToList());
                }

                _assemblyTypes = result;
                return _assemblyTypes;
            }
        }

        public IEnumerable<Type> SupportedInterfaces
        {
            get
            {
                yield return typeof(IPing);
                yield return typeof(IPong);

            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterMediator(builder);
            foreach (var i in SupportedInterfaces)
                RegisterInterface(builder, i);

            RegisterQueryHandlers(builder);
            builder.RegisterType<MyApplication>().As<IMyApplication>();
        }

        private static void RegisterQueryHandlers(ContainerBuilder builder)
        {
            var handlers = AssemblyTypes.Where(tp => !(tp.BaseType is null) && tp.BaseType.FullName.StartsWith("MediatR.RequestHandler`"));
            foreach (Type t in handlers)
            {
                Console.WriteLine($"Registering handlers {t.Name}");
                builder.RegisterType(t).AsImplementedInterfaces();
            }
        }

        private static void RegisterInterface(ContainerBuilder builder, Type interfaceType)
        {
            var iTypes = AssemblyTypes.Where(tp => tp.GetInterface(interfaceType.FullName) != null);
            foreach (Type t in iTypes)
            {
                Console.WriteLine($"Registering named instance {t.Name}");
                builder.RegisterType(t).Named(t.Name, interfaceType);
            }
        }

        private static void RegisterMediator(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}