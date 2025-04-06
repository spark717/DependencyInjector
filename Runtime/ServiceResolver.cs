using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark
{
    internal class ServiceResolver
    {
        public readonly Dictionary<Type, HashSet<Type>> ServiceTypesByBaseType = new();

        public NewServiceCollection ServiceCollection;
        

        public void RegisterTypePair<TServ, TBase>()
        {
            var baseType = typeof(TBase);
            var serviceType = typeof(TServ);

            if (baseType.IsAssignableFrom(serviceType) == false)
                throw new Exception();
            
            var list = ServiceTypesByBaseType.GetOrCreate(baseType);
            list.Add(serviceType);
        }
        
        public TBase Resolve<TBase>()
        {
            return (TBase)Resolve(typeof(TBase));
        }
        
        public TBase[] ResolveMany<TBase>()
        {
            return (TBase[])Resolve(typeof(TBase[]));
        }
        
        public object[] Resolve(IEnumerable<Type> types)
        {
            return types.Select(Resolve).ToArray();
        }
        
        public object Resolve(Type type)
        {
            var isArray = type.IsArray;
            var baseType = isArray ? type.GetElementType() : type;
            if (isArray)
            {
                var serviceTypes = ServiceTypesByBaseType.GetOrCreate(baseType);
                var instances = serviceTypes.Select(GetOrCreateInstance);
                var array = CreateArray(baseType, instances);
                return array;
            }
            else
            {
                var serviceType = ServiceTypesByBaseType.GetOrCreate(baseType).FirstOrDefault();
                if (serviceType == null)
                    throw new Exception();
                
                var instance = GetOrCreateInstance(serviceType);
                return instance;
            }
        }

        private object GetOrCreateInstance(Type serviceType)
        {
            var controller = ServiceCollection.GetActiveController(serviceType);
            var instance = controller.GetOrCreateInstance();
            return instance;
        }
        
        private object CreateArray(Type baseType, IEnumerable<object> instances) 
        {
            var length = instances.Count();
            var array = Array.CreateInstance(baseType, length);

            var i = 0;
            
            foreach (var instance in instances)
            {
                array.SetValue(instance, i);
                i++;
            }
            
            return array;
        }
    }
}