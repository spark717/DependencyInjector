using System;
using System.Linq;
using System.Reflection;

namespace Spark
{
    internal class ServiceFactory
    {
        public ServiceResolver Resolver;
        
        public TServ CreateServiceWithReflection<TServ>()
        {
            var constructor = typeof(TServ).GetConstructors(BindingFlags.Public).FirstOrDefault();
            if (constructor != null)
            {
                // TODO add infinity recursion guard
            
                var argsTypes = constructor.GetParameters().Select(x => x.ParameterType);
                var args = argsTypes.Select(Resolver.Resolve).ToArray();
                var instance = constructor.Invoke(args);
                return (TServ)instance;
            }
            else
            {
                var instance = Activator.CreateInstance<TServ>();
                return instance;
            }
        }
    }
}