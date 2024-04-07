using System;
using Spark;

namespace Tests.Editor.Fakes
{
    public class Installer : IServiceInstaller
    {
        private readonly Action<IServiceBinder> _action;

        public Installer(Action<IServiceBinder> action)
        {
            _action = action;
        }
        
        public void Install(IServiceBinder binder)
        {
            _action(binder);
        }
    }
}