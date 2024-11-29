using System;

namespace Spark
{
    public interface IServiceBinder
    {
        public void Bind<TServ>(bool isSingletone = true);
        public void Bind<TServ>(Func<TServ> factory, bool isSingletone = true);
        public void Bind<TServ>(Func<IDependencyInjector, TServ> factory, bool isSingletone = true);
        public void Bind<TServ>(TServ instance);
        public void Bind<TBase, TServ>(bool isSingletone = true) where TServ : TBase;
        public void Bind<TBase, TServ>(Func<TServ> factory, bool isSingletone = true) where TServ : TBase;
        public void Bind<TBase, TServ>(Func<IDependencyInjector, TServ> factory, bool isSingletone = true) where TServ : TBase;
        public void Bind<TBase, TServ>(TServ instance) where TServ : TBase;
    }
}