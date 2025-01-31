using App.Data.Entities;

namespace SimoshStore;

public class ProductDTO
{
    public int SellerId { get; set; }
    public int CategoryId { get; set; }
    public int? DiscountId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public byte StockAmount { get; set; }
    public bool Enabled { get; set; } = true;

    // Navigation properties
    public UserEntity Seller { get; set; } = null!;

    public CategoryEntity Category { get; set; } = null!;
    public DiscountEntity? Discount { get; set; }

    public ICollection<ProductImageEntity> Images { get; set; } = null!;
    public ICollection<ProductCommentEntity> Comments { get; set; } = null!;
}
