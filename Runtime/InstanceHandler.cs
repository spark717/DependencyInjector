using System;

namespace Spark
{
    internal class InstanceHandler
    {
        public ProcessorsCollection ProcessorsCollection;
        
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
            ProcessorsCollection.OnServiceCreated(instance);
            return instance;
        }

        public void DestroyInstance(ServiceModelBase model)
        {
            var instance = model.GetInstance();
            if (instance != null)
                ProcessorsCollection.OnServiceDestroyed(instance);
            if (instance is IDisposable dis)
                dis.Dispose();
            model.DestroyInstance();
        }
    }
}