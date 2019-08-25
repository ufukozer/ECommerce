using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Interface;

namespace ECommerce.Adapter
{
    public class ContactAdapter : ICrud
    {
        public void Delete<T>(int id) where T : class
        {
            T model = Find<T>(id);
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                eCommerceContext.Set<T>().Remove(model);
                eCommerceContext.SaveChanges();
            }
        }

        public T Find<T>(int id) where T : class
        {
            T contact;
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                contact = eCommerceContext.Set<T>().Find(id);
            }
            return contact;
        }

        public IQueryable<T> Get<T>() where T : class
        {
            IQueryable<T> models;
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                models = eCommerceContext.Set<T>();
            }
            return models;
        }

        public T Insert<T>(T model) where T : class
        {
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                eCommerceContext.Set<T>().Add(model);
                eCommerceContext.SaveChanges();
            }
            return model;
        }

        public T Update<T>(T model) where T : class
        {
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                eCommerceContext.Set<T>().Update(model);
                eCommerceContext.SaveChanges();
            }
            return model;
        }
    }
}
