using System;

namespace Spark
{
    public interface IServiceBindingSetup
    {
        public IServiceBindingSetup As<T>();
        public IServiceBindingSetup As(Type type);
        public IServiceBindingSetup AsProcessor();
        public IServiceBindingSetup AsFallbackResolver();
        public IServiceBindingSetup WithFactory(Func<object> factory);
        public IServiceBindingSetup WithFactory(Func<IDependencyInjector, object> factory);
        public IServiceBindingSetup WithInstance(object instance);
        public Type GetServiceType();
    }
}