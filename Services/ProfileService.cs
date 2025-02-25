using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimoshStore;


public class ProfileService : IProfileService
{
    private readonly IDataRepository _Repository;
    public ProfileService(IDataRepository repository)
    {
        _Repository = repository;
    }
    public async Task<UserEntity> GetUserByIdAsync(int userId)
    {
        var user = await _Repository.GetByIdAsync<UserEntity>(userId);
        if (user == null)
        {
            return new UserEntity();
        }
        return user;
    }
    public async Task<ProductCommentEntity> GetCommentByIdAsync(int userId)
    {
        var comment = await _Repository.GetByIdAsync<ProductCommentEntity>(userId);
        if (comment == null)
        {
            return new ProductCommentEntity();
        }
        return comment;
    }
    public async Task<IEnumerable<ProductCommentEntity>> GetComments()
    {
        var comments = await _Repository.GetAll<ProductCommentEntity>().ToListAsync();
        if (comments == null)
        {
            return new List<ProductCommentEntity>();
        }
        return comments;
    }
}
