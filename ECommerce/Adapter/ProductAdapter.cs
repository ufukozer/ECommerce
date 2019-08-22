using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Interface;

namespace ECommerce.Adapter
{
    public class ProductAdapter : ICrud
    {
        public void Delete<T>(int id) where T : class
        {
            T model = Find<T>(id);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Set<T>().Remove(model);
                eCommerceContext.SaveChanges();
            }
        }

        public T Find<T>(int id) where T : class
        {
            T product;
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                product = eCommerceContext.Set<T>().Find(id);
            }
            return product;
        }

        public T Insert<T>(T model) where T : class
        {
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Set<T>().Add(model);
                eCommerceContext.SaveChanges();
            }
            return model;
        }

        public T Update<T>(T model) where T : class
        {
            using(ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Set<T>().Update(model);
                eCommerceContext.SaveChanges();
            }
            return model;
        }
    }
}
