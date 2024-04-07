using NUnit.Framework;
using Spark;
using Tests.Editor.Fakes;

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
            binder.Bind<Service>(isTransient: true);
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
            binder.Bind<Service>(() => new Service(), isTransient: true);
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
            binder.Bind<IService, Service>(isTransient: true);
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
            binder.Bind<IService, Service>(() => new Service(), isTransient: true);
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
}
