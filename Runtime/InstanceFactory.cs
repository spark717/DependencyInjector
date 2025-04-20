namespace Spark
{
    internal class InstanceFactory<TServ> : IFactory<TServ>
    {
        public TServ Instance;
        
        public TServ Create()
        {
            return Instance;
        }
    }
}