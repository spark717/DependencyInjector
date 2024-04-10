namespace Spark
{
    public interface IDependencyInjector
    {
        public TBase Resolve<TBase>();
        public TBase[] ResolveMany<TBase>();
        public void Inject(IServiceInjectable target);
    }
}