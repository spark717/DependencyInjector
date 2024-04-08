namespace Spark
{
    internal class SingletoneHandler
    {
        public ServiceCollection Collection;
        
        public void CreateSingletones()
        {
            foreach (var model in Collection.GetModels())
            {
                if (model.IsSingletone && model.Scope.IsEnabled)
                {
                    model.GetInstance();
                }
            }
        }
        
        public void DestroySingletones()
        {
            foreach (var model in Collection.GetModels())
            {
                if (model.IsSingletone && model.Scope.IsEnabled == false)
                {
                    model.DestroyInstance();
                }
            }
        }
    }
}