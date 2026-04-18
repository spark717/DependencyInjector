using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark
{
    internal class ServiceResolver
    {
        public readonly Dictionary<Type, HashSet<Type>> ServiceTypesByBaseType = new();
        
        public FallbackServiceResolver Fallback;
        public ServiceCollection ServiceCollection;
        
        public void RegisterTypePair(Type serviceType, Type baseType)
        {
            if (baseType.IsAssignableFrom(serviceType) == false)
                throw new Exception($"Type {serviceType} is not derived from type {baseType}");
            
            var list = ServiceTypesByBaseType.GetOrCreate(baseType);
            list.Add(serviceType);
        }

        public bool CanResolve<TBase>()
        {
            return CanResolve(typeof(TBase));
        }
        
        public bool CanResolve(Type type)
        {
            var serviceTypes = ServiceTypesByBaseType.GetOrCreate(type);
            var hasAnyActiveController = serviceTypes.Any(HasActiveController);
            if (hasAnyActiveController)
                return true;

            return Fallback.CanResolve(type);
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
            try
            {
                return ResolveInternal(type);
            }
            catch (Exception e)
            {
                var result = Fallback.Resolve(type);
                if (result != null)
                    return result;
                else
                    throw;
            }
        }
        
        private object ResolveInternal(Type type)
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