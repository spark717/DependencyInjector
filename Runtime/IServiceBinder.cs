namespace Spark
{
    public interface IServiceBinder
    {
        public IServiceBindingSetup<TServ> Bind<TServ>(bool isSingletone = true);
    }
}