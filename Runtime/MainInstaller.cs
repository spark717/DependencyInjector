namespace Spark
{
    internal class MainInstaller : IServiceInstaller
    {
        private readonly DependencyInjector _di;

        public MainInstaller(DependencyInjector di)
        {
            _di = di;
        }

        public void Install(IServiceBinder binder)
        {
            binder
                .Bind<DependencyInjector>()
                .As<IDependencyInjector>()
                .WithInstance(_di);
        }
    }
}