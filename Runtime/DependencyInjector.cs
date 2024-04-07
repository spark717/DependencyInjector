namespace Spark
{
    public class DependencyInjector
    {
        private readonly ServiceCollection _collection;
        private readonly ServiceResolver _resolver;
        private readonly ServiceBinder _binder;
        private readonly ServiceFactory _factory;
        private readonly CircularDependencyGuard _guard;

        public DependencyInjector()
        {
            _collection = new ServiceCollection();
            _resolver = new ServiceResolver();
            _factory = new ServiceFactory();
            _binder = new ServiceBinder();
            _guard = new CircularDependencyGuard();

            _resolver.ServiceCollection = _collection;
            _resolver.Guard = _guard;
            _binder.ServiceCollection = _collection;
            _binder.ServiceFactory = _factory;
            _factory.Resolver = _resolver;
        }
        
        public void Install(params IServiceInstaller[] installers)
        {
            foreach (var installer in installers)
            {
                installer.Install(_binder);
            }
        }

        public TServ Resolve<TServ>()
        {
            return _resolver.Resolve<TServ>();
        }

        public void CreateAllInstances()
        {
            
        }
        
        public void DestroyAllInstances()
        {
            
        }
    }
}