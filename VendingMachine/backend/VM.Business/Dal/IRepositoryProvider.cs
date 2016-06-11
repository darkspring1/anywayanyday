using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Business.Dal
{
    public interface IRepositoryProvider
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
