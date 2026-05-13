using System;
using System.Linq;
using System.Reflection;

namespace Spark
{
    internal class ServiceInjector
    {
        public ServiceResolver Resolver;
        
        public void Inject(IServiceInjectable target)
        {
            var method = GetInjectMethod(target);
            if (method == null)
                throw new Exception($"Inject method is missing in {target.GetType().Name}");

            var argsTypes = method.GetParameters().Select(x => x.ParameterType).ToArray();
            var args = Resolver.Resolve(argsTypes);
            method.Invoke(target, args);
        }

        private MethodInfo GetInjectMethod(object target)
        {
            return GetInjectMethod(target.GetType());
        }

        private MethodInfo GetInjectMethod(Type type)
        {
            var currentType = type;
            while (currentType != null)
            {
                var method = currentType.GetMethod("Inject", 
                    BindingFlags.Instance | 
                    BindingFlags.Public | 
                    BindingFlags.NonPublic | 
                    BindingFlags.DeclaredOnly);

                if (method != null)
                    return method;

                currentType = currentType.BaseType;
            }

            return null;
        }
    }
}