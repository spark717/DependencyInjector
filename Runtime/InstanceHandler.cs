using System;

namespace Spark
{
    internal class InstanceHandler
    {
        public ProcessorsCollection ProcessorsCollection;
        public ServiceInjector Injector;
        
        public object GetInstance(ServiceModelBase model)
        {
            if (model.IsSingletone)
            {
                var instance = model.GetInstance();
                if (instance == null)
                    instance = CreateNewInstance(model);
                
                return instance;
            }
            else
            {
                var instance = CreateNewInstance(model);
                return instance;
            }
        }

        private object CreateNewInstance(ServiceModelBase model)
        {
            var instance = model.CreateNewInstance();
            if (instance is IServiceInjectable injectable)
                Injector.Inject(injectable);
            
            if (instance is IServiceCreateable creatable)
                creatable.OnCreate();
            
            ProcessorsCollection.OnServiceCreated(instance);
            
            return instance;
        }

        public void DestroyInstance(ServiceModelBase model)
        {
            var instance = model.GetInstance();
            if (instance is IServiceDestroyable destroyable)
                destroyable.OnDestroy();
            
            if (instance is IDisposable dis)
                dis.Dispose();
            
            if (instance != null)
                ProcessorsCollection.OnServiceDestroyed(instance);
            
            model.DestroyInstance();
        }
    }
}