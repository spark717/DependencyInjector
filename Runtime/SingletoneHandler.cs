namespace Spark
{
    internal class SingletoneHandler
    {
        public ServiceCollection Collection;
        public InstanceHandler InstanceHandler;
        
        public void CreateSingletones()
        {
            foreach (var model in Collection.GetModels())
            {
                if (model.IsSingletone && model.Scope.IsEnabled)
                {
                    InstanceHandler.GetInstance(model);
                }
            }
        }
        
        public void DestroySingletones()
        {
            foreach (var model in Collection.GetModels())
            {
                if (model.IsSingletone && model.Scope.IsEnabled == false)
                {
                    InstanceHandler.DestroyInstance(model);
                }
            }
        }
    }
}