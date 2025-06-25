using System;

namespace Spark
{
    internal class ServiceBindingSetup<TServ> : IServiceBindingSetup<TServ>
    {
        public ServiceController<TServ> Controller;
        public ServiceResolver ServiceResolver;
        public IDependencyInjector DependencyInjector;
        public AutoBindingController AutoBindingController;
        public FallbackServiceResolver FallbackServiceResolver;

        public bool IsAutoSelfBindingCanceled;

        public void Init()
        {
            AutoBindingController.Add<TServ>();
        }
        
        public IServiceBindingSetup<TServ> As<TBase>()
        {
            ServiceResolver.RegisterTypePair<TServ, TBase>();
            AutoBindingController.Remove<TServ>();
            return this;
        }
        
        public IServiceBindingSetup<TServ> AsProcessor()
        {
            As<IServiceProcessor>();
            Controller.IsProcessor = true;
            return this;
        }

        public IServiceBindingSetup<TServ> AsFallbackResolver()
        {
            As<IServiceResolver>();
            FallbackServiceResolver.AddController(Controller);
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
            Controller.Factory = new InstanceFactory<TServ>()
            {
                Instance = instance
            };
            return this;
        }
    }
}