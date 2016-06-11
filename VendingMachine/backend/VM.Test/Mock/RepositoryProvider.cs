using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Business.Dal;
using VM.Business.Entities;

namespace VM.Test.Mock
{
    class RepositoryProvider : IRepositoryProvider
    {
        public IRepository<T> GetRepository<T>() where T : class
        {
            var entityType = typeof(T);

            if (entityType == typeof(Good))
            {
                return (IRepository<T>)new GoodRepository();
            }

            if (entityType == typeof(User))
            {
                return (IRepository<T>)new UserRepository();
            }

            if (entityType == typeof(VendingMachine))
            {
                return (IRepository<T>)new VendingMachineRepository();
            }
            throw new NotImplementedException();
        }
    }
}
