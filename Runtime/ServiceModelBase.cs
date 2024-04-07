namespace Spark
{
    internal abstract class ServiceModelBase
    {
        public bool IsRegistered;

        public abstract object GetInstance();
    }
}