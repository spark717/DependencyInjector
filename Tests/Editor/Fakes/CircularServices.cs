namespace Tests.Editor.Fakes
{
    public static class CircularServices
    {
        public class ServiceA
        {
            public ServiceB B { get; }

            public ServiceA(ServiceB b)
            {
                B = b;
            }
        }
        
        public class ServiceB
        {
            public ServiceC C { get; }
            public ServiceD D { get; }

            public ServiceB(ServiceC c, ServiceD d)
            {
                C = c;
                D = d;
            }
        }
        
        public class ServiceC
        {
            public ServiceB B { get; }

            public ServiceC(ServiceB b)
            {
                B = b;
            }
        }
        
        public class ServiceD
        {
            public ServiceC C { get; }

            public ServiceD(ServiceC c)
            {
                C = c;
            }
        }
    }
}