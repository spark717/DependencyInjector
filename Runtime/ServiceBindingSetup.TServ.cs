using System;

namespace Spark
{
    internal class ServiceBindingSetup<TServ> : ServiceBindingSetup, IServiceBindingSetup<TServ>
    {
        public IServiceBindingSetup<TServ> As<T>()
        {
            As(typeof(T));
            return this;
        }

        public IServiceBindingSetup<TServ> WithFactory(Func<TServ> factory)
        {
            Controller.Factory = new FuncFactory()
            {
                Func = () => factory(),
            };
            return this;
        }

        public IServiceBindingSetup<TServ> WithFactory(Func<IDependencyInjector, TServ> factory)
        {
            Controller.Factory = new FuncInjectorFactory()
            {
                Func = di => factory(di),
                DependencyInjector = DependencyInjector,
            };
            return this;
        }

        public IServiceBindingSetup<TServ> WithInstance(TServ instance)
        {
            Controller.Factory = new InstanceFactory()
            {
                Instance = instance
            };
            return this;
        }

        public new IServiceBindingSetup<TServ> AsProcessor()
        {
            base.AsProcessor();
            return this;
        }

        public new IServiceBindingSetup<TServ> AsFallbackResolver()
        {
            base.AsFallbackResolver();
            return this;
        }
    }
}