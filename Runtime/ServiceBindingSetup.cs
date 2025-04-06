using System;

namespace Spark
{
    internal class ServiceBindingSetup<TServ> : IServiceBindingSetup<TServ>
    {
        public ServiceScopeController<TServ> Controller;
        public ServiceResolver ServiceResolver;
        public IDependencyInjector DependencyInjector;

        public IServiceBindingSetup<TServ> As<TBase>()
        {
            ServiceResolver.RegisterTypePair<TServ, TBase>();
            return this;
        }
        
        public IServiceBindingSetup<TServ> AsProcessor()
        {
            ServiceResolver.RegisterTypePair<TServ, IServiceProcessor>();
            Controller.IsProcessor = true;
            return this;
        }

        public IServiceBindingSetup<TServ> WithFactory(Func<TServ> factory)
        {
            Controller.Factory = new FuncFactory<TServ>()
            {
                Func = factory
            };
            return this;
        }

        public IServiceBindingSetup<TServ> WithFactory(Func<IDependencyInjector, TServ> factory)
        {
            Controller.Factory = new FuncInjectorFactory<TServ>()
            {
                Func = factory,
                DependencyInjector = DependencyInjector,
            };
            return this;
        }

        public IServiceBindingSetup<TServ> WithInstance(TServ instance)
        {
            if (Controller.IsSingletone == false)
                throw new Exception();
            
            Controller.Instance = instance;
            return this;
        }
    }
}