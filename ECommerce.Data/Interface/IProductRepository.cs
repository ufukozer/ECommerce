

namespace ECommerce.Data.Interface
{
    public interface IProductRepository
    {
        Data.Models.Product Insert(Data.Models.Product product);
        Data.Models.Product Update(Data.Models.Product product);
        void Delete(int productId);
        Data.Models.Product Find(int productId);
    }
}
