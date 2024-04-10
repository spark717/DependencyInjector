namespace Spark
{
    public class DependencyInjector : IDependencyInjector
    {
        private readonly ServiceCollection _collection;
        private readonly ServiceResolver _resolver;
        private readonly ServiceFactory _factory;
        private readonly CircularDependencyGuard _guard;
        private readonly SingletoneHandler _singletoneHandler;
        private readonly ServiceInjector _injector;
        private readonly ApplicationScope _defaultScope;
        private readonly ProcessorsCollection _processorsCollection;
        private readonly InstanceHandler _instanceHandler;

        public DependencyInjector()
        {
            _collection = new ServiceCollection();
            _resolver = new ServiceResolver();
            _factory = new ServiceFactory();
            _guard = new CircularDependencyGuard();
            _singletoneHandler = new SingletoneHandler();
            _injector = new ServiceInjector();
            _defaultScope = new ApplicationScope();
            _processorsCollection = new ProcessorsCollection();
            _instanceHandler = new InstanceHandler();

            _resolver.ServiceCollection = _collection;
            _resolver.Guard = _guard;
            _resolver.InstanceHandler = _instanceHandler;
            _factory.Resolver = _resolver;
            _singletoneHandler.Collection = _collection;
            _singletoneHandler.InstanceHandler = _instanceHandler;
            _instanceHandler.ProcessorsCollection = _processorsCollection;
            _instanceHandler.Injector = _injector;
            _injector.Resolver = _resolver;
            
            Install(new MainInstaller(this));
        }
        
        public void Install(IServiceInstaller installer, IServiceScope scope = null)
        {
            if (scope == null)
                scope = _defaultScope;
            
            var binder = new ServiceBinder
            {
                Scope = scope,
                ServiceCollection = _collection,
                ServiceFactory = _factory
            };

            installer.Install(binder);
        }

        public void AddProcessor(IServiceProcessor processor)
        {
            _processorsCollection.Add(processor);
        }
        
        public TBase Resolve<TBase>()
        {
            return _resolver.Resolve<TBase>();
        }
        
        public TBase[] ResolveMany<TBase>()
        {
            return _resolver.ResolveMany<TBase>();
        }

        public void Inject(IServiceInjectable target)
        {
            _injector.Inject(target);
        }

        public void CreateSingletones()
        {
            _singletoneHandler.CreateSingletones();
        }
        
        public void DestroySingletones()
        {
            _singletoneHandler.DestroySingletones();
        }
    }
}