using ECommerce.Data.Interface;
using ECommerce.Data.Models;


namespace ECommerce.Service
{
    public class ProductRepository : IProductRepository
    {
        public void Delete(int productId)
        {
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                Product product = Find(productId);
                eCommerceContext.Products.Remove(product);
                eCommerceContext.SaveChanges();
            }
        }

        public Product Find(int productId)
        {
            Product product;
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                product = eCommerceContext.Products.Find(productId);
            }
            return product;
        }

        public Product Insert(Product product)
        {
            using(Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                eCommerceContext.Products.Add(product);
                eCommerceContext.SaveChanges();
            }
            return product;
        }

        public Product Update(Product product)
        {
            using(Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                eCommerceContext.Products.Update(product);
            }
            return product;
        }
    }
}
