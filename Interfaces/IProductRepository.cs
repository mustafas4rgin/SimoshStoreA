using App.Data.Entities;

namespace SimoshStore;

public interface IProductRepository
{
    public List<ProductEntity> GetAllProducts();
}
