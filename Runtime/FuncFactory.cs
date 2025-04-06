using System;

namespace Spark
{
    internal class FuncFactory<TServ> : IFactory<TServ>
    {
        public Func<TServ> Func;
        
        public TServ Create()
        {
            return Func();
        }
    }
}