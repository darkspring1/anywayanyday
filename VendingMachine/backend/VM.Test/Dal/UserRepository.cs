using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Business.Dal;
using VM.Business.Entities;

namespace VM.Test.Mock
{
    class UserRepository : IRepository<User>
    {


        public static List<User> Users = new List<User>
        {
            new User { Id = 1, Wallet = new Wallet { r1 = 100, r2 = 100, r5 = 100, r10 = 100 } },
            new User { Id = 2, Wallet = new Wallet() }
        };


        public IQueryable<User> GetAll()
        {
            return Users.AsQueryable();
        }

        public User GetById(object key)
        {
            return Users.FirstOrDefault(u => u.Id == (int)key);
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            
        }
    }
}
