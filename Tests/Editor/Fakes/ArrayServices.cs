namespace Tests.Editor.Fakes
{
    public static class ArrayServices
    {
        public class ServiceA
        {
            public IService[] Services { get; }

            public ServiceA(IService[] services)
            {
                Services = services;
            }
        }

        public class ServiceB : IService
        {
            public object Obj { get; set; }
        }
        
        public class ServiceC : IService
        {
            public object Obj { get; set; }
        }
    }
}