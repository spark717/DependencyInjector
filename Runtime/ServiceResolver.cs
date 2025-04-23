using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark
{
    internal class ServiceResolver
    {
        public readonly Dictionary<Type, HashSet<Type>> ServiceTypesByBaseType = new();

        public ServiceCollection ServiceCollection;

        public void RegisterSelf<TServ>()
        {
            var serviceType = typeof(TServ);
            var list = ServiceTypesByBaseType.GetOrCreate(serviceType);
            list.Add(serviceType);
        }
        
        public void UnregisterSelf<TServ>()
        {
            var serviceType = typeof(TServ);
            var list = ServiceTypesByBaseType.GetOrCreate(serviceType);
            list.Remove(serviceType);
        }

        public void RegisterTypePair<TServ, TBase>()
        {
            var baseType = typeof(TBase);
            var serviceType = typeof(TServ);

            if (baseType.IsAssignableFrom(serviceType) == false)
                throw new Exception();
            
            var list = ServiceTypesByBaseType.GetOrCreate(baseType);
            list.Add(serviceType);

            UnregisterSelf<TServ>();
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
            var serviceTypes = ServiceTypesByBaseType.GetOrCreate(baseType);
            if (isArray)
            {
                var instances = serviceTypes
                    .Where(HasActiveController)
                    .Select(GetOrCreateInstance);
                var array = CreateArray(baseType, instances);
                return array;
            }
            else
            {
                var serviceType = serviceTypes.FirstOrDefault(HasActiveController);
                if (serviceType == null)
                    throw new Exception($"Missing active binding for resolving type {baseType}");
                
                var instance = GetOrCreateInstance(serviceType);
                return instance;
            }
        }

        private bool HasActiveController(Type serviceType)
        {
            return ServiceCollection.HasActiveController(serviceType);
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