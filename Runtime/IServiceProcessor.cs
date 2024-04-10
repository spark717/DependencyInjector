namespace Spark
{
    public interface IServiceProcessor
    {
        public void OnServiceCreated(object service);
        public void OnServiceDestroyed(object service);
    }
}