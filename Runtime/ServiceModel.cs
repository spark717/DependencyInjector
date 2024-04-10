using System;

namespace Spark
{
    internal class ServiceModel<TServ> : ServiceModelBase
    {
        public TServ Instance;
        public Func<TServ> CreateInstance;
        
        public override object GetInstance()
        {
            return Instance;
        }

        public override object CreateNewInstance()
        {
            var instance = CreateInstance();

            if (IsSingletone)
                Instance = instance;

            return instance;
        }

        public override void DestroyInstance()
        {
            Instance = default;
        }
    }
}