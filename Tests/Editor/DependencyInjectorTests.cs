using System;
using NUnit.Framework;
using Spark;
using Tests.Editor.Fakes;
using UnityEngine;

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
}
