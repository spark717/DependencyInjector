using System;
using System.Linq;
using System.Reflection;

namespace Spark
{
    internal class ReflectionFactory : IFactory
    {
        public ServiceResolver Resolver;
        public Type ServiceType;
        
        public object Create()
        {
            var constructor = ServiceType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
            if (constructor != null)
            {
                var argsTypes = constructor.GetParameters().Select(x => x.ParameterType);
                var args = Resolver.Resolve(argsTypes);
                var instance = constructor.Invoke(args);
                return instance;
            }
            else
            {
                if (ServiceType.IsAbstract)
                    throw new Exception($"Cant create instance of abstract type <{ServiceType.Name}>");
                
                var instance = Activator.CreateInstance(ServiceType);
                return instance;
            }
        }
    }
}