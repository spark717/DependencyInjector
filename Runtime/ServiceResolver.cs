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
                throw new Exception($"Missing binding for type <{baseType.Name}>{Guard.GetHistoryString()}");
            
            var servType = binding.ServiceTypeList.First();

            var model = ServiceCollection.GetModel(servType);
            if (model == null)
                throw new Exception($"Missing model for binding <{baseType.Name}>->{servType.Name}{Guard.GetHistoryString()}");

            if (model.Scope.IsEnabled == false)
                throw new Exception($"Scope <{model.Scope.GetType().Name}> is disabled for binding <{baseType.Name}>->{servType.Name}{Guard.GetHistoryString()}");
            
            Guard.Append(servType);
            var instance = model.GetInstance();
            Guard.RemoveTop();
            return instance;
        }
    }
}