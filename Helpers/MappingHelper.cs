using App.Data.Entities;

namespace SimoshStore;

public class MappingHelper
{
    public static BlogCategoryEntity MappingBlogCategoryEntity(BlogCategoryEntityDTO dto)
    {
        return new BlogCategoryEntity
        {
             Name = dto.Name,
        };
    }
    public static BlogEntity MappingBlogEntity(BlogDTO dto)
    {
        return new BlogEntity
        {
            Title = dto.Title,
            Content = dto.Content,
            ImageUrl = dto.ImageUrl,
            UserId = dto.UserId,
        };
    }
    public static CategoryEntity MappingCategory(CategoryDTO dto)
    {
        return new CategoryEntity
        {
            Color = dto.Color,
            IconCssClass = dto.IconCssClass,
            Name = dto.Name
        };
    }
    public static ProductEntity MappingProduct(ProductDTO dto)
    {
        return new ProductEntity
        {
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            DiscountId = dto.DiscountId,
            Discount = dto.Discount,
            Price = dto.Price,
            Name = dto.Name,
            StockAmount = dto.StockAmount,
            Enabled = dto.Enabled,
            Category = dto.Category,
            Images = dto.Images,
            Comments = dto.Comments
        };
    }
    public static ProductImageEntity MappingProductImage(ProductImageDTO dto)
    {
        return new ProductImageEntity
        {
            Url = dto.Url,
            ProductId = dto.ProductId
        };
    }
}
