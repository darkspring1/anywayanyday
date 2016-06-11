using StructureMap;
using VM.Business.Dal;
using VM.Dal.EF;

namespace VM.Api.StructureMap
{
    public class VMRegistry : Registry
    {
        public VMRegistry()
        {

            For<DataContext>().Use<DataContext>().Ctor<string>("connectionString").Is("vm");
            For<IRepositoryProvider>().Use<RepositoryProvider>();

        }
    }
}
