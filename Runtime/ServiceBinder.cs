using System;

namespace Spark
{
    internal class ServiceBinder : IServiceBinder
    {
        public IServiceScope Scope;
        public ServiceCollection ServiceCollection;
        public ServiceResolver Resolver;
        public FallbackServiceResolver FallbackServiceResolver;
        public DependencyInjector DependencyInjector;
        public ServiceInjector Injector;
        public ProcessorsCollection ProcessorsCollection;
        public CircularDependencyGuard Guard;
        public AutoBindingController AutoBindingController;
        
        public IServiceBindingSetup Bind<TServ>(bool isSingletone = true)
        {
            return Bind(typeof(TServ), isSingletone);
        }

        public IServiceBindingSetup Bind(Type serviceType, bool isSingletone = true)
        {
            var controller = new ServiceController()
            {
                IsSingletone = isSingletone,
                Scope = Scope,
                Injector = Injector,
                ProcessorsCollection = ProcessorsCollection,
                Guard = Guard,
                ServiceType = serviceType,
                Factory = new ReflectionFactory()
                {
                    Resolver = Resolver,
                    ServiceType = serviceType,
                }
            };
            ServiceCollection.Add(controller);
            
            var setup = new ServiceBindingSetup
            {
                Controller = controller,
                ServiceResolver = Resolver,
                DependencyInjector = DependencyInjector,
                AutoBindingController = AutoBindingController,
                FallbackServiceResolver = FallbackServiceResolver,
                ServiceType = serviceType,
            };
            setup.Init();
            return setup;
        }
    }
}