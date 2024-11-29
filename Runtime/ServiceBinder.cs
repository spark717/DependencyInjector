using System;

namespace Spark
{
    internal class ServiceBinder : IServiceBinder
    {
        public ServiceCollection ServiceCollection;
        public ServiceFactory ServiceFactory;
        public IServiceScope Scope;
        public IDependencyInjector DependencyInjector;
        
        public void Bind<TServ>(bool isSingletone)
        {
            AddBinding<TServ, TServ>();
            RegisterService<TServ>(isSingletone);
        }

        public void Bind<TServ>(Func<TServ> factory, bool isSingletone)
        {
            AddBinding<TServ, TServ>();
            RegisterService(factory, isSingletone);
        }

        public void Bind<TServ>(Func<IDependencyInjector, TServ> factory, bool isSingletone = true)
        {
            AddBinding<TServ, TServ>();
            RegisterService(factory, isSingletone);
        }

        public void Bind<TServ>(TServ instance)
        {
            AddBinding<TServ, TServ>();
            RegisterService(instance);
        }

        public void Bind<TBase, TServ>(bool isSingletone) where TServ : TBase
        {
            AddBinding<TBase, TServ>();
            RegisterService<TServ>(isSingletone);
        }

        public void Bind<TBase, TServ>(Func<TServ> factory, bool isSingletone) where TServ : TBase
        {
            AddBinding<TBase, TServ>();
            RegisterService(factory, isSingletone);
        }

        public void Bind<TBase, TServ>(Func<IDependencyInjector, TServ> factory, bool isSingletone = true) where TServ : TBase
        {
            AddBinding<TBase, TServ>();
            RegisterService(factory, isSingletone);
        }

        public void Bind<TBase, TServ>(TServ instance) where TServ : TBase
        {
            AddBinding<TBase, TServ>();
            RegisterService(instance);
        }

        private void RegisterService<TServ>(bool isSingletone)
        {
            if (typeof(TServ).IsAbstract)
                throw new Exception($"Cant bind abstract type <{typeof(TServ).Name}> to self");
            
            RegisterService(ServiceFactory.CreateServiceWithReflection<TServ>, isSingletone);
        }
        
        private void RegisterService<TServ>(Func<TServ> factory, bool isSingletone)
        {
            var model = GetOrCreateModel<TServ>();
            model.CreateInstance = factory;
            model.IsSingletone = isSingletone;
            model.Scope = Scope;
        }
        
        private void RegisterService<TServ>(Func<IDependencyInjector, TServ> factory, bool isSingletone)
        {
            RegisterService(() => factory(DependencyInjector), isSingletone);
        }
        
        private void RegisterService<TServ>(TServ instance)
        {
            var model = GetOrCreateModel<TServ>();
            model.Instance = instance;
            model.IsSingletone = true;
            model.Scope = Scope;
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