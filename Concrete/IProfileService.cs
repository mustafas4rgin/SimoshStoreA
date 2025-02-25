using App.Data.Entities;

namespace SimoshStore;

public interface IProfileService
{
    public Task<UserEntity> GetUserByIdAsync(int userId);
    public Task<ProductCommentEntity> GetCommentByIdAsync(int commentId);
    public Task<IEnumerable<ProductCommentEntity>> GetComments();
}
