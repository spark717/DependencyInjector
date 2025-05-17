using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark
{
    public class DependencyInjector : IDependencyInjector
    {
        private readonly ServiceCollection _collection;
        private readonly ServiceResolver _resolver;
        private readonly CircularDependencyGuard _guard;
        private readonly ServiceInjector _injector;
        private readonly ApplicationScope _defaultScope;
        private readonly ProcessorsCollection _processorsCollection;
        private readonly AutoBindingController _autoBindingController;

        public DependencyInjector()
        {
            _collection = new ServiceCollection();
            _resolver = new ServiceResolver();
            _guard = new CircularDependencyGuard();
            _injector = new ServiceInjector();
            _defaultScope = new ApplicationScope();
            _processorsCollection = new ProcessorsCollection();
            _autoBindingController = new AutoBindingController();

            _resolver.ServiceCollection = _collection;
            _injector.Resolver = _resolver;
            _autoBindingController.ServiceResolver = _resolver;
            
            Install(new MainInstaller(this));
        }
        
        public void Install(IServiceInstaller installer, IServiceScope scope = null)
        {
            if (scope == null)
                scope = _defaultScope;
            
            var binder = new ServiceBinder
            {
                Scope = scope,
                ServiceCollection = _collection,
                Resolver = _resolver,
                DependencyInjector = this,
                ProcessorsCollection = _processorsCollection,
                Injector = _injector,
                Guard = _guard,
                AutoBindingController = _autoBindingController,
            };

            installer.Install(binder);
            
            _autoBindingController.Execute();
        }

        public void AddFallback(IDependencyInjector fallbackDi)
        {
            _resolver.AddFallback(((DependencyInjector)fallbackDi)._resolver);
        }
        
        public TBase Resolve<TBase>()
        {
            return _resolver.Resolve<TBase>();
        }
        
        public TBase[] ResolveMany<TBase>()
        {
            return _resolver.ResolveMany<TBase>();
        }

        public bool CanResolve<TBase>()
        {
            return _resolver.CanResolve<TBase>();
        }

        public void Inject(IServiceInjectable target)
        {
            _injector.Inject(target);
        }

        public void CreateSingletones()
        {
            foreach (var controller in _collection.GetControllers())
            {
                if (controller.IsProcessor())
                {
                    controller.CreateSingletone();
                }
            }
            
            foreach (var controller in _collection.GetControllers())
            {
                controller.CreateSingletone();
            }
        }
        
        public void DestroySingletones()
        {
            foreach (var controller in _collection.GetControllers())
            {
                controller.DestroySingletone();
            }
        }
    }
}