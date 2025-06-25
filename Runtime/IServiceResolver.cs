using System;

namespace Spark
{
    public interface IServiceResolver
    {
        public object Resolve(Type type);
        public bool CanResolve(Type type);
    }
}