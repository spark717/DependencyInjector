// using System;
//
// namespace Spark
// {
//     internal class InstanceHandler
//     {
//         public ProcessorsCollection ProcessorsCollection;
//         public ServiceInjector Injector;
//         
//         public object GetInstance(IServiceController controller)
//         {
//             if (controller.IsSingletone())
//             {
//                 var instance = controller.GetInstance();
//                 if (instance == null)
//                     instance = CreateNewInstance(controller);
//                 
//                 return instance;
//             }
//             else
//             {
//                 var instance = CreateNewInstance(controller);
//                 return instance;
//             }
//         }
//
//         private object CreateNewInstance(IServiceController controller)
//         {
//             var instance = model.CreateNewInstance();
//             if (instance is IServiceInjectable injectable)
//                 Injector.Inject(injectable);
//             
//             if (instance is IServiceCreateable creatable)
//                 creatable.OnCreate();
//             
//             ProcessorsCollection.OnServiceCreated(instance);
//             
//             return instance;
//         }
//
//         public void DestroyInstance(ServiceModelBase model)
//         {
//             var instance = model.GetInstance();
//             if (instance is IServiceDestroyable destroyable)
//                 destroyable.OnDestroy();
//             
//             if (instance is IDisposable dis)
//                 dis.Dispose();
//             
//             if (instance != null)
//                 ProcessorsCollection.OnServiceDestroyed(instance);
//             
//             model.DestroyInstance();
//         }
//     }
// }