namespace SimoshStore;

public interface IUserService
{
    public Task<IServiceResult> DeleteUserAsync(int id);
    public Task<IServiceResult> GetAllUsersAsync();
    public Task<IServiceResult> GetUserAsync(int id);
    public Task<IServiceResult> UpdateUserAsync(UserDTO userDTO, int id);
    public int GetUserId();
    public Task<IServiceResult> UpdateUserAddress(EditAddressViewModel model);
}
