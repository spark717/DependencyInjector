namespace Spark
{
    internal class ServiceBinder : IServiceBinder
    {
        public IServiceScope Scope;
        public NewServiceCollection ServiceCollection;
        public ServiceResolver Resolver;
        public DependencyInjector DependencyInjector;
        public ServiceInjector Injector;
        public ProcessorsCollection ProcessorsCollection;
        public CircularDependencyGuard Guard;
        
        public IServiceBindingSetup<TServ> Bind<TServ>(bool isSingletone = true)
        {
            var controller = new ServiceScopeController<TServ>()
            {
                IsSingletone = isSingletone,
                Scope = Scope,
                Injector = Injector,
                ProcessorsCollection = ProcessorsCollection,
                Guard = Guard,
                Factory = new ReflectionFactory<TServ>()
                {
                    Resolver = Resolver
                }
            };
            ServiceCollection.Add(controller);
            
            var setup = new ServiceBindingSetup<TServ>
            {
                Controller = controller,
                ServiceResolver = Resolver,
                DependencyInjector = DependencyInjector,
            };
            return setup;
        }
    }
}