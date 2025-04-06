namespace Spark
{
    internal interface IServiceController
    {
        public bool IsSingletone();
        public bool IsActive();
        public object GetOrCreateInstance();
        public bool HasInstance();
        public void DestroySingletone();
        public void CreateSingletone();
        public bool IsProcessor();
    }
}