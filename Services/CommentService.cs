using App.Data.Entities;

namespace SimoshStore;

public class CommentService
{
    private readonly IDataRepository _Repository;
    public CommentService(IDataRepository repository)
    {
        _Repository = repository;
    }
    public async Task<IServiceResult> GettingComments(int productId)
    {
        var comments = _Repository.GetAll<ProductCommentEntity>();
        if(comments == null)
        {
            return new ServiceResult(false, "no comments found");
        }
        return new ServiceResult(true, "comments found");
    }
    public async Task<IServiceResult> AddingCommentToProduct(ProductCommentEntity comment)
    {
        var product = await _Repository.GetByIdAsync<ProductEntity>(comment.ProductId);
        if(product == null)
        {
            return new ServiceResult(false, "product not found");
        }
        await _Repository.AddAsync(comment);
        return new ServiceResult(true, "comment added successfully");
    }
}
