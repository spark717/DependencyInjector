namespace Spark
{
    public class DependencyInjector : IDependencyInjector
    {
        private readonly NewServiceCollection _collection;
        private readonly ServiceResolver _resolver;
        private readonly CircularDependencyGuard _guard;
        private readonly ServiceInjector _injector;
        private readonly ApplicationScope _defaultScope;
        private readonly ProcessorsCollection _processorsCollection;

        public DependencyInjector()
        {
            _collection = new NewServiceCollection();
            _resolver = new ServiceResolver();
            _guard = new CircularDependencyGuard();
            _injector = new ServiceInjector();
            _defaultScope = new ApplicationScope();
            _processorsCollection = new ProcessorsCollection();

            _resolver.ServiceCollection = _collection;
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
                Resolver = _resolver,
                DependencyInjector = this,
                ProcessorsCollection = _processorsCollection,
                Injector = _injector,
                Guard = _guard,
            };

            installer.Install(binder);
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
            foreach (var controller in _collection.GetControllers())
            {
                if (controller.IsProcessor())
                {
                    controller.CreateSingletone();
                }
            }
            
            foreach (var controller in _collection.GetControllers())
            {
                controller.CreateSingletone();
            }
        }
        
        public void DestroySingletones()
        {
            foreach (var controller in _collection.GetControllers())
            {
                controller.DestroySingletone();
            }
        }
    }
}