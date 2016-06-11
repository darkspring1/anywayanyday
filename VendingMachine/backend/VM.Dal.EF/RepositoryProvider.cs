using VM.Business.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Dal.EF
{
    public class RepositoryProvider : IRepositoryProvider
    {

        Dictionary<string, object> _repositories;

        DataContext _context;

        public RepositoryProvider(DataContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        IRepository<T> getRepo<T>(string key) where T : class
        {
            object repo;
            _repositories.TryGetValue(key, out repo);
            return (IRepository<T>)repo;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var key = typeof(T).FullName;
            var repo = getRepo<T>(key);
            if (repo == null)
            {
                repo = new Repository<T>(_context);
                _repositories.Add(key, repo);
            }

            return repo;



        }
    }
}
