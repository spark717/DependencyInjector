using System;

namespace Spark
{
    public interface IServiceBinder
    {
        public IServiceBindingSetup Bind<TServ>(bool isSingletone = true);
        public IServiceBindingSetup Bind(Type serviceType, bool isSingletone = true);
    }
}