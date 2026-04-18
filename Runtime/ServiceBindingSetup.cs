using System;

namespace Spark
{
    internal class ServiceBindingSetup : IServiceBindingSetup
    {
        public ServiceController Controller;
        public ServiceResolver ServiceResolver;
        public IDependencyInjector DependencyInjector;
        public AutoBindingController AutoBindingController;
        public FallbackServiceResolver FallbackServiceResolver;

        public bool IsAutoSelfBindingCanceled;
        public Type ServiceType;

        public void Init()
        {
            AutoBindingController.Add(ServiceType);
        }
        
        public IServiceBindingSetup As(Type type)
        {
            ServiceResolver.RegisterTypePair(serviceType: ServiceType, baseType: type);
            AutoBindingController.Remove(ServiceType);
            return this;
        }
        
        public IServiceBindingSetup AsProcessor()
        {
            As(typeof(IServiceProcessor));
            Controller.IsProcessor = true;
            return this;
        }

        public IServiceBindingSetup AsFallbackResolver()
        {
            As(typeof(IServiceResolver));
            FallbackServiceResolver.AddController(Controller);
            return this;
        }

        public IServiceBindingSetup WithFactory(Func<object> factory)
        {
            Controller.Factory = new FuncFactory()
            {
                Func = factory
            };
            return this;
        }

        public IServiceBindingSetup WithFactory(Func<IDependencyInjector, object> factory)
        {
            Controller.Factory = new FuncInjectorFactory()
            {
                Func = factory,
                DependencyInjector = DependencyInjector,
            };
            return this;
        }

        public IServiceBindingSetup WithInstance(object instance)
        {
            if (instance.GetType() != ServiceType)
            {
                throw new Exception($"Instance type {instance.GetType()} is not the same as registered type {ServiceType}");
            }
            
            Controller.Factory = new InstanceFactory()
            {
                Instance = instance
            };
            return this;
        }

        public Type GetServiceType()
        {
            return ServiceType;
        }
    }
}