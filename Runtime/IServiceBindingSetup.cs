using System;

namespace Spark
{
    public interface IServiceBindingSetup<TServ> : IServiceBindingSetup
    {
        public IServiceBindingSetup<TServ> As<T>();
        public IServiceBindingSetup<TServ> WithFactory(Func<TServ> factory);
        public IServiceBindingSetup<TServ> WithFactory(Func<IDependencyInjector, TServ> factory);
        public IServiceBindingSetup<TServ> WithInstance(TServ instance);
        public new IServiceBindingSetup<TServ> AsProcessor();
        public new IServiceBindingSetup<TServ> AsFallbackResolver();
    }
    
    public interface IServiceBindingSetup
    {
        public IServiceBindingSetup As(Type type);
        public IServiceBindingSetup AsProcessor();
        public IServiceBindingSetup AsFallbackResolver();
        public IServiceBindingSetup WithFactory(Func<object> factory);
        public IServiceBindingSetup WithFactory(Func<IDependencyInjector, object> factory);
        public IServiceBindingSetup WithInstance(object instance);
        public Type GetServiceType();
    }
}