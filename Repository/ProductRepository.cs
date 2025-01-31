using App.Data.Entities;
using AutoMapper;

namespace SimoshStore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;
    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public List<ProductEntity> GetAllProducts()
    {
        List<ProductEntity> productEntities = _appDbContext.Products.ToList();
        return productEntities;
    }
}

