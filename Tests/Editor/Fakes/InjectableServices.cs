using Spark;

namespace Tests.Editor.Fakes
{
    public static class InjectableServices
    {
        public class ServiceA : IServiceInjectable
        {
            public ServiceB B;
            
            public void Inject(ServiceB b)
            {
                B = b;
            }
        }
        
        public class ServiceB : ServiceBase
        {
            public ServiceC C;
            
            public void Inject(ServiceD d, ServiceC c)
            {
                C = c;
                
                base.Inject(d);
            }
        }
        
        public class ServiceC : IServiceInjectable
        {
            public ServiceD D;
            
            public void Inject(ServiceD d)
            {
                D = d;
            }
        }
        
        public class ServiceD
        {
            
        }
        
        public class ServiceE : ServiceBase
        {
            
        }
        
        public class ServiceBase : IServiceInjectable
        {
            public ServiceD D;
            
            public void Inject(ServiceD d)
            {
                D = d;
            }
        }
    }
}