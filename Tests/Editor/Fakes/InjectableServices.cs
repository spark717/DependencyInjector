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
        
        public class ServiceB : IServiceInjectable
        {
            public ServiceD D;
            public ServiceC C;
            
            public void Inject(ServiceD d, ServiceC c)
            {
                D = d;
                C = c;
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
    }
}