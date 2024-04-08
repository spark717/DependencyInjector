using Spark;

namespace Tests.Editor.Fakes
{
    public class Scope : IServiceScope
    {
        public bool IsEnabled { get; set; }
    }
}