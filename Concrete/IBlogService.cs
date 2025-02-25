using App.Data.Entities;

namespace SimoshStore;

public interface IBlogService
{  
    Task<BlogEntity> GetRandomBlog();
    Task<BlogEntity> GetBlogByIdAsync(int blogId);
    Task<IEnumerable<BlogEntity>> GetAllBlogsAsync();
    Task<BlogEntity> CreateBlogAsync(BlogEntity blog);
    Task<BlogEntity> UpdateBlogAsync(BlogEntity blog);
    Task DeleteBlogAsync(int blogId);
    Task AddTagToBlogAsync(int blogId, int tagId);
    Task AddCategoryToBlogAsync(int blogId, int categoryId);
    Task<IEnumerable<BlogCommentEntity>> GetComments(int blogId);
}
    
