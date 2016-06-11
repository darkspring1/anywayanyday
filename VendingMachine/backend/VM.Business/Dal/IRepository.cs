using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Business.Dal
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T GetById(object key);

        void Add(T entity);

        void SaveChanges();
    }
}
