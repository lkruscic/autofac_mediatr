using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;

namespace autofac_mediatR.IoC
{
   public interface IDependencyResolver
   {
      T Resolve<T>(params Parameter[] parameters);
      object Resolve(Type t, params Parameter[] parameters);

      T ResolveKey<T>(string key);
   }
}
