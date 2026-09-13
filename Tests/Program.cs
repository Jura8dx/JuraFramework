using JuraFramework.Tests.Templates;
using Veldrid;

namespace JuraFramework.Tests
{
    internal class Program
    {
        static void Main()
        {
            Application application = new TestApplication("Test Application");
            Engine engine = new Engine(application);
            engine.Run();
        }
    }
}
