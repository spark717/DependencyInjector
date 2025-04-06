using System;

namespace Spark
{
    internal class FuncInjectorFactory<TServ> : IFactory<TServ>
    {
        public Func<IDependencyInjector, TServ> Func;
        public IDependencyInjector DependencyInjector;
        
        public TServ Create()
        {
            return Func(DependencyInjector);
        }
    }
}