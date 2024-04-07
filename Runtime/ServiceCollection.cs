using System;
using System.Collections.Generic;

namespace Spark
{
    internal class ServiceCollection
    {
        private readonly Dictionary<Type, ServiceModelBase> _modelsByServiceType = new Dictionary<Type, ServiceModelBase>();
        private readonly Dictionary<Type, ServiceBindingModel> _bindingsByBaseType = new Dictionary<Type, ServiceBindingModel>();

        public ServiceModel<TServ> GetOrCreateModel<TServ>()
        {
            if (_modelsByServiceType.TryGetValue(typeof(TServ), out var model))
                return (ServiceModel<TServ>)model;

            var newModel = new ServiceModel<TServ>();
            _modelsByServiceType[typeof(TServ)] = newModel;
            return newModel;
        }
        
        public ServiceModelBase GetModel(Type servType)
        {
            if (_modelsByServiceType.TryGetValue(servType, out var model))
                return model;
            
            return default;
        }
        
        public ServiceBindingModel GetOrCreateBinding<TBase>()
        {
            return GetOrCreateBinding(typeof(TBase));
        }
        
        public ServiceBindingModel GetOrCreateBinding(Type baseType)
        {
            if (_bindingsByBaseType.TryGetValue(baseType, out var binding))
                return binding;

            var newBinding = new ServiceBindingModel(baseType);
            _bindingsByBaseType[baseType] = newBinding;
            return newBinding;
        }
        
        public ServiceBindingModel GetBinding(Type baseType)
        {
            if (_bindingsByBaseType.TryGetValue(baseType, out var binding))
                return binding;
            
            return default;
        }
    }
}