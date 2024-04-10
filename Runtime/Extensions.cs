using UnityEngine;

namespace Spark
{
    public static class Extensions
    {
        public static void InjectInAllComponents(this IDependencyInjector injector, GameObject target)
        {
            var components = target.GetComponentsInParent<IServiceInjectable>(includeInactive: true);
            foreach (var component in components)
            {
                injector.Inject(component);
            }
        }
    }
}