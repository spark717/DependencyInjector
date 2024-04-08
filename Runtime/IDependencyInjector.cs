namespace Spark
{
    public interface IDependencyInjector
    {
        TServ Resolve<TServ>();
    }
}