using NUnit.Framework;
using Spark;
using Tests.Editor.Fakes;
using IService = Tests.Editor.Fakes.IService;

public class DependencyInjectorTests
{
    [Test]
    public void ResolveSelfSingleReflective()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>();
        });
        di.Install(installer);

        var service1 = di.Resolve<Service>();
        var service2 = di.Resolve<Service>();

        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 == service2);
    }
    
    [Test]
    public void ResolveSelfSingleFactory()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>(() => new Service());
        });
        di.Install(installer);

        var service1 = di.Resolve<Service>();
        var service2 = di.Resolve<Service>();

        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 == service2);
    }
    
    [Test]
    public void ResolveSelfSingleFactoryWithDependency()
    {
        var di = new DependencyInjector();
        var obj = new object();
        var installer = new Installer(binder =>
        {
            binder.Bind<object>(() => obj);
            binder.Bind<Service>(x =>
            {
                var o = x.Resolve<object>();
                return new Service(){ Obj = o };
            });
        });
        di.Install(installer);

        var service1 = di.Resolve<Service>();
        var service2 = di.Resolve<Service>();

        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1.Obj == obj);
        Assert.True(service1 == service2);
    }
    
    [Test]
    public void ResolveSelfInstance()
    {
        var di = new DependencyInjector();
        var serv = new Service();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>(serv);
        });
        di.Install(installer);

        var service1 = di.Resolve<Service>();
        var service2 = di.Resolve<Service>();

        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(serv == service2);
        Assert.True(serv == service2);
    }
    
    [Test]
    public void ResolveSelfTransientReflective()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>(isSingletone: false);
        });
        di.Install(installer);

        var service1 = di.Resolve<Service>();
        var service2 = di.Resolve<Service>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 != service2);
    }
    
    [Test]
    public void ResolveSelfTransientFactory()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>(() => new Service(), isSingletone: false);
        });
        di.Install(installer);

        var service1 = di.Resolve<Service>();
        var service2 = di.Resolve<Service>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 != service2);
    }
    
    [Test]
    public void ResolveInterfaceSingleReflective()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<IService, Service>();
        });
        di.Install(installer);

        var service1 = di.Resolve<IService>();
        var service2 = di.Resolve<IService>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 == service2);
    }
    
    [Test]
    public void ResolveInterfaceSingleFactory()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<IService, Service>(() => new Service());
        });
        di.Install(installer);

        var service1 = di.Resolve<IService>();
        var service2 = di.Resolve<IService>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 == service2);
    }
    
    [Test]
    public void ResolveInterfaceSingleFactoryWithDependency()
    {
        var di = new DependencyInjector();
        var obj = new object();
        var installer = new Installer(binder =>
        {
            binder.Bind<object>(() => obj);
            binder.Bind<IService, Service>(x =>
            {
                var o = x.Resolve<object>();
                return new Service(){ Obj = o };
            });
        });
        di.Install(installer);

        var service1 = di.Resolve<IService>();
        var service2 = di.Resolve<IService>();

        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1.Obj == obj);
        Assert.True(service1 == service2);
    }
    
    [Test]
    public void ResolveInterfaceTransientReflective()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<IService, Service>(isSingletone: false);
        });
        di.Install(installer);

        var service1 = di.Resolve<IService>();
        var service2 = di.Resolve<IService>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 != service2);
    }
    
    [Test]
    public void ResolveInterfaceTransientFactory()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<IService, Service>(() => new Service(), isSingletone: false);
        });
        di.Install(installer);

        var service1 = di.Resolve<IService>();
        var service2 = di.Resolve<IService>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 != service2);
    }
    
    [Test]
    public void ResolveInterfaceInstance()
    {
        var di = new DependencyInjector();
        var serv = new Service();
        var installer = new Installer(binder =>
        {
            binder.Bind<IService, Service>(serv);
        });
        di.Install(installer);

        var service1 = di.Resolve<IService>();
        var service2 = di.Resolve<IService>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(serv == service1);
        Assert.True(serv == service2);
    }
    
    [Test]
    public void ResolveConstructor()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<ChainServices.ServiceA>();
            binder.Bind<ChainServices.ServiceB>();
            binder.Bind<ChainServices.ServiceC>();
            binder.Bind<ChainServices.ServiceD>();
        });
        di.Install(installer);

        var a = di.Resolve<ChainServices.ServiceA>();

        Assert.True(a != null);
        Assert.True(a.B != null);
        Assert.True(a.B.C != null);
        Assert.True(a.B.D != null);
        Assert.True(a.B.D.C != null);
        Assert.True(a.B.D.C == a.B.C);
    }
    
    [Test]
    public void ExceptionOnCircularDependency()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<CircularServices.ServiceA>();
            binder.Bind<CircularServices.ServiceB>();
            binder.Bind<CircularServices.ServiceC>();
            binder.Bind<CircularServices.ServiceD>();
        });
        di.Install(installer);

        Assert.Catch(() =>
        {
            var a = di.Resolve<CircularServices.ServiceA>();
        });
    }
    
    [Test]
    public void ExceptionOnMissingBinding()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<ChainServices.ServiceA>();
            binder.Bind<ChainServices.ServiceB>();
            binder.Bind<ChainServices.ServiceC>();
        });
        di.Install(installer);

        Assert.Catch(() =>
        {
            var service2 = di.Resolve<ChainServices.ServiceA>();
        });
    }
    
    [Test]
    public void ExceptionOnDisabledScope()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>();
        });
        var scope = new Scope()
        {
            IsEnabled = true,
        };
        di.Install(installer, scope);

        var service = di.Resolve<Service>();
        scope.IsEnabled = false;
        
        Assert.True(service != null);
        Assert.Catch(() =>
        {
            di.Resolve<Service>();
        });
    }
    
    [Test]
    public void DestroySingletone()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>();
        });
        var scope = new Scope()
        {
            IsEnabled = true,
        };
        di.Install(installer, scope);

        var service1 = di.Resolve<Service>();
        scope.IsEnabled = false;
        di.DestroySingletones();
        scope.IsEnabled = true;
        var service2 = di.Resolve<Service>();
        di.DestroySingletones();
        var service3 = di.Resolve<Service>();
        
        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service3 != null);
        Assert.True(service1 != service2);
        Assert.True(service2 == service3);
    }
    
    [Test]
    public void ExceptionOnAbstractService()
    {
        var di = new DependencyInjector();

        Assert.Catch(() =>
        {
            var installer = new Installer(binder =>
            {
                binder.Bind<IService>();
            });
            di.Install(installer);
            di.Resolve<IService>();
        });
    }
    
    [Test]
    public void ResolveInjector()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>();
        });
        di.Install(installer);
        var di2 = di.Resolve<IDependencyInjector>();
        var service1 = di.Resolve<Service>();
        var service2 = di2.Resolve<Service>();

        Assert.True(service1 != null);
        Assert.True(service2 != null);
        Assert.True(service1 == service2);
    }
    
    [Test]
    public void ResolveArray()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<ArrayServices.ServiceA>();
            binder.Bind<IService, ArrayServices.ServiceB>();
            binder.Bind<IService, ArrayServices.ServiceC>();
        });
        di.Install(installer);

        var a = di.Resolve<ArrayServices.ServiceA>();

        Assert.True(a != null);
        Assert.True(a.Services.Length == 2);
        Assert.True(a.Services[0] != null);
        Assert.True(a.Services[1] != null);
        Assert.True(a.Services[0].GetType() != a.Services[1].GetType());
        Assert.True(a.Services[0] != a.Services[1]);
    }
    
    [Test]
    public void ResolveArrayInScopes()
    {
        var di = new DependencyInjector();
        var scope1 = new Scope() { IsEnabled = false };
        var scope2 = new Scope() { IsEnabled = false };
        var installer1 = new Installer(binder =>
        {
            binder.Bind<IService, ArrayServices.ServiceB>();
        });
        var installer2 = new Installer(binder =>
        {
            binder.Bind<IService, ArrayServices.ServiceC>();
        });
        di.Install(installer1, scope1);
        di.Install(installer2, scope2);

        var arr0 = di.ResolveMany<IService>();
        scope1.IsEnabled = true;
        scope2.IsEnabled = false;
        var arr1 = di.ResolveMany<IService>();
        scope1.IsEnabled = false;
        scope2.IsEnabled = true;
        var arr2 = di.ResolveMany<IService>();
        scope1.IsEnabled = true;
        scope2.IsEnabled = true;
        var arr3 = di.ResolveMany<IService>();
        
        Assert.True(arr0.Length == 0);
        Assert.True(arr1.Length == 1);
        Assert.True(arr2.Length == 1);
        Assert.True(arr3.Length == 2);
        Assert.True(arr1[0] is ArrayServices.ServiceB);
        Assert.True(arr2[0] is ArrayServices.ServiceC);
        Assert.True(arr3[0].GetType() != arr3[1].GetType());
        Assert.True(arr3[0] != arr3[1]);
    }
    
    [Test]
    public void CreateAndDestroyProcess()
    {
        var di = new DependencyInjector();
        var scope = new Scope() { IsEnabled = false};
        var processor = new Processor();
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>();
        });
        di.Install(installer, scope);
        di.AddProcessor(processor);
        scope.IsEnabled = true;
        di.CreateSingletones();
        scope.IsEnabled = false;
        di.DestroySingletones();
        
        Assert.True(processor.CreateProcessedCount == 1);
        Assert.True(processor.DestroyProcessedCount == 1);
    }
    
    [Test]
    public void InjectInMethod()
    {
        var di = new DependencyInjector();
        var installer = new Installer(binder =>
        {
            binder.Bind<InjectableServices.ServiceB>();
            binder.Bind<InjectableServices.ServiceC>();
            binder.Bind<InjectableServices.ServiceD>();
        });
        di.Install(installer);

        var a = new InjectableServices.ServiceA();
        di.Inject(a);
        
        Assert.True(a.B != null);
        Assert.True(a.B.C != null);
        Assert.True(a.B.D != null);
        Assert.True(a.B.C.D != null);
        Assert.True(a.B.C.D == a.B.D);
    }
    
    [Test]
    public void CreateAndDestroyMessageHandle()
    {
        var di = new DependencyInjector();
        var scope = new Scope() { IsEnabled = false };
        var installer = new Installer(binder =>
        {
            binder.Bind<Service>();
        });
        di.Install(installer, scope);

        scope.IsEnabled = true;
        var service = di.Resolve<Service>();
        scope.IsEnabled = false;
        di.DestroySingletones();
        
        Assert.IsTrue(service.IsCreated);
        Assert.IsTrue(service.IsDestroyed);
        Assert.IsTrue(service.IsDisposed);
    }
}
