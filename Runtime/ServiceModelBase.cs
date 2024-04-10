namespace Spark
{
    internal abstract class ServiceModelBase
    {
        public bool IsSingletone;
        public IServiceScope Scope;

        public abstract object GetInstance();
        public abstract object CreateNewInstance();
        public abstract void DestroyInstance();
    }
}