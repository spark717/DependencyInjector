using System;

namespace Spark
{
    public interface IServiceBinder
    {
        public IServiceBindingSetup<TServ> Bind<TServ>(bool isSingletone = true);
        public IServiceBindingSetup Bind(Type serviceType, bool isSingletone = true);
    }
}