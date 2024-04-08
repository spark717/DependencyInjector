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
            var constructor = typeof(TServ).GetConstructors(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
            if (constructor != null)
            {
                var argsTypes = constructor.GetParameters().Select(x => x.ParameterType);
                var args = argsTypes.Select(Resolver.Resolve).ToArray();
                var instance = constructor.Invoke(args);
                return (TServ)instance;
            }
            else
            {
                if (typeof(TServ).IsAbstract)
                    throw new Exception($"Cant create instance of abstract type <{typeof(TServ).Name}>");
                
                var instance = Activator.CreateInstance<TServ>();
                return instance;
            }
        }
    }
}