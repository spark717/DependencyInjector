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

        public DependencyInjector()
        {
            _collection = new ServiceCollection();
            _resolver = new ServiceResolver();
            _factory = new ServiceFactory();
            _guard = new CircularDependencyGuard();
            _singletoneHandler = new SingletoneHandler();
            _injector = new ServiceInjector();
            _defaultScope = new ApplicationScope();

            _resolver.ServiceCollection = _collection;
            _resolver.Guard = _guard;
            _factory.Resolver = _resolver;
            _singletoneHandler.Collection = _collection;
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

        public TServ Resolve<TServ>()
        {
            return _resolver.Resolve<TServ>();
        }

        public void Inject(object target)
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