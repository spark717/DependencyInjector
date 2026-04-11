using System;
using System.Collections.Generic;

namespace Spark
{
    internal class AutoBindingController
    {
        public ServiceResolver ServiceResolver;
        
        public readonly HashSet<Type> Services = new();

        public void Add(Type serviceType)
        {
            Services.Add(serviceType);
        }

        public void Remove(Type serviceType)
        {
            Services.Add(serviceType);
        }

        public void Execute()
        {
            foreach (var serviceType in Services)
            {
                ServiceResolver.RegisterTypePair(serviceType, serviceType);
            }
            
            Services.Clear();
        }
    }
}