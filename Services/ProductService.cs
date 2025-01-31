using App.Data.Entities;

namespace SimoshStore;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository){
        _repository = repository;
    }
    public List<ProductEntity> GetProducts()
    {
        return _repository.GetAllProducts();
    }
}
