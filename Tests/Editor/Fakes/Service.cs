using System;
using Spark;

namespace Tests.Editor.Fakes
{
    public class Service : IService, IServiceCreateable, IServiceDestroyable, IDisposable
    {
        public bool IsCreated;
        public bool IsDestroyed;
        public bool IsDisposed;
        
        public void OnCreate()
        {
            IsCreated = true;
        }

        public void OnDestroy()
        {
            IsDestroyed = true;
        }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}