using System;
using System.Linq;

namespace Spark
{
    internal class ServiceResolver
    {
        public ServiceCollection ServiceCollection;

        public Type Context;

        public TServ Resolve<TServ>()
        {
            return (TServ)Resolve(typeof(TServ));
        }
        
        public object Resolve(Type baseType)
        {
            var binding = ServiceCollection.GetBinding(baseType);
            if (binding == null)
                throw new Exception("Missing binding");
            
            var servType = binding.ServiceTypeList.First();

            var model = ServiceCollection.GetModel(servType);
            if (model == null)
                throw new Exception("Missing model");

            if (model.IsRegistered == false)
                throw new Exception("Not registered");

            var instance = model.GetInstance();
            return instance;
        }
    }
}