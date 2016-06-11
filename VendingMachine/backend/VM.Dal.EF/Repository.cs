using VM.Business.Dal;
using System.Data.Entity;
using System.Linq;

namespace VM.Dal.EF
{
    class Repository<T> : IRepository<T> where T : class
    {

        DbSet<T> _set;

        DataContext _context;
        public Repository(DataContext context)
        {
            _set = context.Set<T>();
            _context = context;
        }


        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public T GetById(object key)
        {
            return _set.Find(key);
        }

        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
