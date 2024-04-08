using System;

namespace Spark
{
    internal class ServiceModel<TServ> : ServiceModelBase
    {
        public TServ Instance;
        public Func<TServ> CreateInstance;
        
        public override object GetInstance()
        {
            if (IsSingletone == false)
                return CreateInstance();
            
            if (Instance == null)
                Instance = CreateInstance();

            return Instance;
        }

        public override void DestroyInstance()
        {
            if (Instance is IDisposable dis)
                dis.Dispose();
            
            Instance = default;
        }
    }
}