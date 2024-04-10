using Spark;

namespace Tests.Editor.Fakes
{
    public class Processor : IServiceProcessor
    {
        public int CreateProcessedCount;
        public int DestroyProcessedCount;
        
        public void OnServiceCreated(object service)
        {
            if (service is Service)
                CreateProcessedCount += 1;
        }

        public void OnServiceDestroyed(object service)
        {
            if (service is Service)
                DestroyProcessedCount += 1;
        }
    }
}