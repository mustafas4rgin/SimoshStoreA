using App.Data.Entities;

namespace SimoshStore;

public interface IProductService
{
    public List<ProductEntity> GetProducts();
}
