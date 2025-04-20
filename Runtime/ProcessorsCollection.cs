using System.Collections.Generic;

namespace Spark
{
    internal class ProcessorsCollection
    {
        private readonly List<IServiceProcessor> _processors = new List<IServiceProcessor>();

        public void OnServiceCreated(object service)
        {
            if (service is IServiceProcessor serviceProcessor)
            {
                _processors.Add(serviceProcessor);
            }
            else
            {
                foreach (var processor in _processors)
                {
                    processor.OnServiceCreated(service);
                }
            }
        }
        
        public void OnServiceDestroyed(object service)
        {
            if (service is IServiceProcessor serviceProcessor)
            {
                _processors.Remove(serviceProcessor);
            }
            else
            {
                foreach (var processor in _processors)
                {
                    processor.OnServiceDestroyed(service);
                }
            }
        }
    }
}