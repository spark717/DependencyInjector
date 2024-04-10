using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark
{
    internal class ServiceResolver
    {
        public ServiceCollection ServiceCollection;
        public CircularDependencyGuard Guard;
        public InstanceHandler InstanceHandler;

        public TBase Resolve<TBase>()
        {
            return (TBase)Resolve(typeof(TBase));
        }
        
        public TBase[] ResolveMany<TBase>()
        {
            return (TBase[])Resolve(typeof(TBase[]));
        }
        
        public object Resolve(Type type)
        {
            var isArray = type.IsArray;
            var baseType = isArray ? type.GetElementType() : type;
            
            var binding = ServiceCollection.GetBinding(baseType);
            if (binding == null)
                throw new Exception($"Missing binding for type <{baseType.Name}>{Guard.GetHistoryString()}");

            if (isArray)
                return ResolveArray(baseType, binding.ServiceTypeList);
            else
                return ResolveSingle(baseType, binding.ServiceTypeList.First());
        }

        public object[] Resolve(IEnumerable<Type> types)
        {
            return types.Select(Resolve).ToArray();
        }

        private object ResolveArray(Type baseType, List<Type> servTypes) 
        {
            var length = servTypes.Count(IsTypeSuitableForArrayResolve);
            var array = Array.CreateInstance(baseType, length);

            var i = 0;
            
            foreach (var servType in servTypes)
            {
                if (IsTypeSuitableForArrayResolve(servType) == false)
                    continue;
                
                var instance = ResolveSingle(baseType, servType);
                array.SetValue(instance, i);
                i++;
            }
            
            return array;
        }

        private object ResolveSingle(Type baseType, Type servType)
        {
            var model = ServiceCollection.GetModel(servType);
            if (model == null)
                throw new Exception($"Missing model for binding <{baseType.Name}>->{servType.Name}{Guard.GetHistoryString()}");
            
            if (model.Scope.IsEnabled == false)
                throw new Exception($"Scope <{model.Scope.GetType().Name}> is disabled for binding <{baseType.Name}>->{servType.Name}{Guard.GetHistoryString()}");
            
            Guard.Append(servType);
            var instance = InstanceHandler.GetInstance(model);
            Guard.RemoveTop();
            return instance;
        }
        
        private bool IsTypeSuitableForArrayResolve(Type servType)
        {
            return ServiceCollection.GetModel(servType).Scope.IsEnabled;
        }
    }
}