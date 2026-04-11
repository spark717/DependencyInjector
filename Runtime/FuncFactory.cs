using System;

namespace Spark
{
    internal class FuncFactory : IFactory
    {
        public Func<object> Func;
        
        public object Create()
        {
            return Func();
        }
    }
}