using System;
using System.Linq;

namespace Spark
{
    internal class ServiceResolver
    {
        public ServiceCollection ServiceCollection;
        public CircularDependencyGuard Guard;

        public TServ Resolve<TServ>()
        {
            return (TServ)Resolve(typeof(TServ));
        }
        
        public object Resolve(Type baseType)
        {
            var binding = ServiceCollection.GetBinding(baseType);
            if (binding == null)
                throw new Exception($"Missing binding for type <{baseType.Name}>");
            
            var servType = binding.ServiceTypeList.First();

            var model = ServiceCollection.GetModel(servType);
            if (model == null)
                throw new Exception($"Missing model <{baseType.Name}>->{servType.Name}");

            if (model.IsRegistered == false)
                throw new Exception($"Model not registered <{baseType.Name}>->{servType.Name}");

            Guard.Append(servType);
            var instance = model.GetInstance();
            Guard.RemoveTop();
            return instance;
        }
    }
}