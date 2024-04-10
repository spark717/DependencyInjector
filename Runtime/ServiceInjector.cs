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
            var method = GetMethod(target);
            if (method == null)
                throw new Exception($"Inject method is missing in {target.GetType().Name}");

            var argsTypes = method.GetParameters().Select(x => x.ParameterType).ToArray();
            var args = Resolver.Resolve(argsTypes);
            method.Invoke(target, args);
        }

        private MethodInfo GetMethod(object target)
        {
            return target.GetType().GetMethod("Inject", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }
    }
}