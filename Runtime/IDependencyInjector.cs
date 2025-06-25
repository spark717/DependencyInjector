namespace Spark
{
    public interface IDependencyInjector : IServiceResolver
    {
        public TBase Resolve<TBase>();
        public TBase[] ResolveMany<TBase>();
        public bool CanResolve<TBase>();
        public void Inject(IServiceInjectable target);
    }
}