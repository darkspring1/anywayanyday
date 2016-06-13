using StructureMap;
using VM.Business.Dal;
using VM.Test.Dal;

namespace VM.Test.Ioc
{
    public class TestRegistry : Registry
    {
        public TestRegistry()
        {
            For(typeof(IRepository<>)).Use(typeof(TestRepository<>));
        }
    }
}
