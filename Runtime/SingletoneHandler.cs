// namespace Spark
// {
//     internal class SingletoneHandler
//     {
//         public NewServiceCollection Collection;
//         
//         public void CreateSingletones()
//         {
//             foreach (var controller in Collection.GetActiveControllers())
//             {
//                 controller.CreateInstance();
//             }
//         }
//         
//         public void DestroySingletones()
//         {
//             foreach (var controller in Collection.GetActiveControllers())
//             {
//                 controller.DestroySingletone();
//             }
//         }
//     }
// }