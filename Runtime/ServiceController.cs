using System;

namespace Spark
{
    internal class ServiceController : IServiceController
    {
        public bool IsSingletone;
        public bool IsProcessor;
        public IServiceScope Scope;
        public object Instance;
        public IFactory Factory;
        public ServiceInjector Injector;
        public ProcessorsCollection ProcessorsCollection;
        public CircularDependencyGuard Guard;
        public Type ServiceType;

        public object GetOrCreateInstance()
        {
            if (IsSingletone)
            {
                CreateSingletone();
                
                return Instance;
            }
            else
            {
                return CreateService();
            }
        }
        
        public void CreateSingletone()
        {
            if (IsSingletone && IsActive() && Instance == null)
            {
                CreateInstance();
            }
        }

        bool IServiceController.IsProcessor()
        {
            return IsProcessor;
        }

        private void CreateInstance()
        {
            Instance = CreateService();
            
            ProcessorsCollection.OnServiceCreated(Instance);
        }

        private object CreateService()
        {
            Guard.Append(ServiceType);
            
            var service = Factory.Create();
            
            if (service is IServiceInjectable injectable)
                Injector.Inject(injectable);
            
            if (service is IServiceCreateable createable)
                createable.OnCreate();
            
            Guard.RemoveTop();
            
            return service;
        }

        public void DestroySingletone()
        {
            if (IsSingletone && IsActive() == false && Instance != null)
            {
                DestroyInstance();
            }
        }

        private void DestroyInstance()
        {
            var instance = Instance;
            Instance = default;
                
            ProcessorsCollection.OnServiceDestroyed(instance);
            
            if (instance is IServiceDestroyable destroyable)
                destroyable.OnDestroy();
            
            if (instance is IDisposable disposable)
                disposable.Dispose();
        }

        bool IServiceController.IsSingletone()
        {
            return IsSingletone;
        }

        public bool IsActive()
        {
            return Scope.IsEnabled;
        }
        
        public bool HasInstance()
        {
            return Instance != null;
        }
    }
}