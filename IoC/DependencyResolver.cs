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
    public class DependencyResolver : IDependencyResolver
    {
        private IContainer _container;
        public static DependencyResolver Instance = new DependencyResolver();


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

        public T ResolveNamed<T>(string name)
        {
            return _container.ResolveNamed<T>(name);
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterModule<AppServiceModule>();

            var appMOdules = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
               .GetFiles()
               .Where(x => x.Name.StartsWith("autofac_mediat") && (x.Name.EndsWith("exe") || x.Name.EndsWith("dll")))
               .Select(dll => Assembly.LoadFile(dll.FullName)).ToArray();

            builder.RegisterAssemblyModules(appMOdules);
            builder.RegisterInstance(this).As<IDependencyResolver>().SingleInstance();
            _container = builder.Build();
        }
    }
}