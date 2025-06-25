using System;
using System.Collections.Generic;

namespace Spark
{
    internal class FallbackServiceResolver
    {
        public readonly List<IServiceController> Controllers = new();

        public void AddController(IServiceController controller)
        {
            Controllers.Add(controller);
        }

        public bool CanResolve(Type type)
        {
            foreach (var controller in Controllers)
            {
                var resolver = GetResolver(controller);
                if (resolver != null && resolver.CanResolve(type))
                    return true;
            }

            return false;
        }

        public object Resolve(Type type)
        {
            foreach (var controller in Controllers)
            {
                var resolver = GetResolver(controller);
                if (resolver != null && resolver.CanResolve(type))
                    return resolver.Resolve(type);
            }

            return default;
        }

        private IServiceResolver GetResolver(IServiceController controller)
        {
            if (controller.IsActive() == false)
                return null;
            
            return (IServiceResolver)controller.GetOrCreateInstance();
        }
    }
}