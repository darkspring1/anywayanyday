using StructureMap;
using VM.Business.Dal;
using VM.Dal.EF;

namespace VM.Api.Ioc
{
    public class VmRegistry : Registry
    {
        public VmRegistry()
        {
            For<DataContext>().Use<DataContext>().Ctor<string>("connectionString").Is("vm");
            For(typeof(IRepository<>)).Use(typeof(Repository<>));
        }
    }
}
