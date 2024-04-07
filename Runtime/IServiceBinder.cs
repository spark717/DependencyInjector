using System;

namespace Spark
{
    public interface IServiceBinder
    {
        public void Bind<TServ>(bool isTransient = false);
        public void Bind<TServ>(Func<TServ> factory, bool isTransient = false);
        public void Bind<TServ>(TServ instance);
        public void Bind<TBase, TServ>(bool isTransient = false) where TServ : TBase;
        public void Bind<TBase, TServ>(Func<TServ> factory, bool isTransient = false) where TServ : TBase;
        public void Bind<TBase, TServ>(TServ instance) where TServ : TBase;
    }
}