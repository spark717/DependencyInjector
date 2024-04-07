using System;

namespace Spark
{
    internal class ServiceBinder : IServiceBinder
    {
        public ServiceCollection ServiceCollection;
        public ServiceFactory ServiceFactory;
        
        public void Bind<TServ>(bool isTransient)
        {
            AddBinding<TServ, TServ>();
            RegisterService<TServ>(isTransient);
        }

        public void Bind<TServ>(Func<TServ> factory, bool isTransient)
        {
            AddBinding<TServ, TServ>();
            RegisterService(factory, isTransient);
        }

        public void Bind<TServ>(TServ instance)
        {
            AddBinding<TServ, TServ>();
            RegisterService(instance);
        }

        public void Bind<TBase, TServ>(bool isTransient) where TServ : TBase
        {
            AddBinding<TBase, TServ>();
            RegisterService<TServ>(isTransient);
        }

        public void Bind<TBase, TServ>(Func<TServ> factory, bool isTransient) where TServ : TBase
        {
            AddBinding<TBase, TServ>();
            RegisterService(factory, isTransient);
        }

        public void Bind<TBase, TServ>(TServ instance) where TServ : TBase
        {
            AddBinding<TBase, TServ>();
            RegisterService(instance);
        }

        private void RegisterService<TServ>(bool isTransient = false)
        {
            RegisterService(ServiceFactory.CreateServiceWithReflection<TServ>, isTransient);
        }
        
        private void RegisterService<TServ>(Func<TServ> factory, bool isTransient = false)
        {
            var model = GetOrCreateModel<TServ>();
            model.CreateInstance = factory;
            model.IsTransient = isTransient;
            model.IsRegistered = true;
        }
        
        private void RegisterService<TServ>(TServ instance)
        {
            var model = GetOrCreateModel<TServ>();
            model.Instance = instance;
            model.IsRegistered = true;
        }

        private void AddBinding<TBase, TServ>()
        {
            var binding = GetOrCreateBinding<TBase>();
            binding.ServiceTypeList.Add(typeof(TServ));
        }

        private ServiceModel<TServ> GetOrCreateModel<TServ>()
        {
            return ServiceCollection.GetOrCreateModel<TServ>();
        }
        
        private ServiceBindingModel GetOrCreateBinding<TServ>()
        {
            return ServiceCollection.GetOrCreateBinding<TServ>();
        }
    }
}