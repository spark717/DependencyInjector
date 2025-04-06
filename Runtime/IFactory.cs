namespace Spark
{
    internal interface IFactory<TServ>
    {
        public TServ Create();
    }
}