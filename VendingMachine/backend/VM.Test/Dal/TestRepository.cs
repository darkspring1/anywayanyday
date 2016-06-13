using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VM.Business.Dal;

namespace VM.Test.Dal
{
    class TestRepository<T> : IRepository<T>
    {
        readonly List<T> _set;
        public TestRepository()
        {
            _set = DataContext.GetData<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _set.AsQueryable();
        }

        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public T GetById(object key)
        {
            Func<T, bool> f = (e) => {
                                         var dynamicEntity = ToDictionary(e);
                                         string id = dynamicEntity["Id"].ToString();
                                         return id == key.ToString();
            };

            return _set
                .Where(f).FirstOrDefault();
        }


        public void SaveChanges()
        {

        }

        IDictionary<string, object> ToDictionary(object value)
        {
            IDictionary<string, object> expando = new Dictionary<string, object>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            {
                expando.Add(property.Name, property.GetValue(value));
            }
            return expando;
        }
    }
}