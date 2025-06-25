using System;

namespace Spark
{
    public interface IServiceBindingSetup<TServ>
    {
        public IServiceBindingSetup<TServ> As<T>();
        public IServiceBindingSetup<TServ> AsProcessor();
        public IServiceBindingSetup<TServ> AsFallbackResolver();
        public IServiceBindingSetup<TServ> WithFactory(Func<TServ> factory);
        public IServiceBindingSetup<TServ> WithFactory(Func<IDependencyInjector, TServ> factory);
        public IServiceBindingSetup<TServ> WithInstance(TServ instance);
    }
}