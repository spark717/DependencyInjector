using System;

namespace Spark
{
    internal class ServiceModel<TServ> : ServiceModelBase
    {
        public TServ Instance;
        public Func<TServ> CreateInstance;
        public bool IsTransient;
        
        public override object GetInstance()
        {
            if (IsTransient)
                return CreateInstance();
            
            if (Instance == null)
                Instance = CreateInstance();

            return Instance;
        }
    }
}