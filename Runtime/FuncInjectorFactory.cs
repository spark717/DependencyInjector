using System;

namespace Spark
{
    internal class FuncInjectorFactory : IFactory
    {
        public Func<IDependencyInjector, object> Func;
        public IDependencyInjector DependencyInjector;
        
        public object Create()
        {
            return Func(DependencyInjector);
        }
    }
}