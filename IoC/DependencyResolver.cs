using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace autofac_mediatR.IoC
{
    class DependencyResolver : IDependencyResolver
    {
        public static DependencyResolver Instance = new DependencyResolver();
        private IContainer _container;

        public DependencyResolver()
        {
            BuildContainer();
        }

        public T Resolve<T>(params Parameter[] parameters)
        {
            return _container.Resolve<T>(parameters);
        }

        public object Resolve(Type t, params Parameter[] parameters)
        {
            return _container.Resolve(t, parameters);
        }

        public T ResolveKey<T>(string key)
        {
            return _container.ResolveKeyed<T>(key);
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterAssemblyModules(assemblies);
            builder.RegisterInstance(this).As<IDependencyResolver>().SingleInstance();

            _container = builder.Build();
        }
    }
}
