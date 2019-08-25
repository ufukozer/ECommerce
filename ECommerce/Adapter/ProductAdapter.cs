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
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                eCommerceContext.Set<T>().Remove(model);
                eCommerceContext.SaveChanges();
            }
        }

        public T Find<T>(int id) where T : class
        {
            T product;
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                product = eCommerceContext.Set<T>().Find(id);
            }
            return product;
        }

        public IQueryable<T> Get<T>() where T : class
        {
            //IQueryable<T> models; 
            //using(ECommerceContext eCommerceContext = new ECommerceContext())
            //{
            //    models = eCommerceContext.Set<T>();
            //}
            //return models;

            Data.ECommerceContext eCommerceContext = new Data.ECommerceContext();
            return eCommerceContext.Set<T>();
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
            using(Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                eCommerceContext.Set<T>().Update(model);
                eCommerceContext.SaveChanges();
            }
            return model;
        }
    }
}
