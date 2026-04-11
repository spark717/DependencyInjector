namespace Spark
{
    internal class InstanceFactory : IFactory
    {
        public object Instance;
        
        public object Create()
        {
            return Instance;
        }
    }
}