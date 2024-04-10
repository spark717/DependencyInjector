using System.Collections.Generic;

namespace Spark
{
    internal class ProcessorsCollection
    {
        private readonly List<IServiceProcessor> _processors = new List<IServiceProcessor>();

        public void Add(IServiceProcessor processor)
        {
            _processors.Add(processor);
        }

        public void OnServiceCreated(object service)
        {
            foreach (var processor in _processors)
            {
                processor.OnServiceCreated(service);
            }
        }
        
        public void OnServiceDestroyed(object service)
        {
            foreach (var processor in _processors)
            {
                processor.OnServiceDestroyed(service);
            }
        }
    }
}