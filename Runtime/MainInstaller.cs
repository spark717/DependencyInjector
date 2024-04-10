namespace Spark
{
    internal class MainInstaller : IServiceInstaller
    {
        private readonly IDependencyInjector _di;

        public MainInstaller(IDependencyInjector di)
        {
            _di = di;
        }

        public void Install(IServiceBinder binder)
        {
            binder.Bind<IDependencyInjector>(_di);
        }
    }
}