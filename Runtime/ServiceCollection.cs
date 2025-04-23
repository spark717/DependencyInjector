using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark
{
    internal class ServiceCollection
    {
        public readonly Dictionary<Type, List<IServiceController>> ControllersByType = new();

        public void Add<TServ>(ServiceController<TServ> controller)
        {
            var list = ControllersByType.GetOrCreate(typeof(TServ));
            list.Add(controller);
        }

        public IServiceController GetActiveController(Type serviceType)
        {
            var list = ControllersByType.GetOrCreate(serviceType);

            var controller = list.FirstOrDefault(x => x.IsActive());
            if (controller == null)
                throw new Exception($"Missing active controller for service type {serviceType}");
            
            return controller;
        }
        
        public bool HasActiveController(Type serviceType)
        {
            var list = ControllersByType.GetOrCreate(serviceType);
            return list.Any(x => x.IsActive());
        }

        public IEnumerable<IServiceController> GetControllers()
        {
            foreach (var controllerList in ControllersByType.Values)
            {
                foreach (var controller in controllerList)
                {
                    yield return controller;
                }
            }
        }
    }
}