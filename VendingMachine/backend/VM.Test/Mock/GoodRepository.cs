using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Business.Dal;
using VM.Business.Entities;

namespace VM.Test.Mock
{
    public class GoodRepository : IRepository<Good>
    {


        public static List<Good> Goods = new List<Good>() {
                new Good { Id = 1, Name = "Чай", Count = 0, Price = 13 },
                new Good { Id = 2, Name = "Кофе", Count = 1, Price = 18 },
                new Good { Id = 3, Name = "Кофе с молоком", Count = 1, Price = 20 },
                new Good { Id = 4, Name = "Сок", Count = 1, Price = 35 }
            };

        public IQueryable<Good> GetAll()
        {
            return Goods.AsQueryable();
        }

        public Good GetById(object key)
        {
            return Goods.FirstOrDefault(g => g.Id == (int)key);
        }

        public void Add(Good entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
        }
    }

}
