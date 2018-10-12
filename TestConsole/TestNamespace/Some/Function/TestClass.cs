using System.Linq;

namespace TestConsole.TestNamespace.Some.Function
{
    public class TestClass
    {
        public string GetFunctionName()
            => this.GetType().Namespace.Split('.').Last();
    }
}
