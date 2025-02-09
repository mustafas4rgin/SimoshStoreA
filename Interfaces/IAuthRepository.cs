namespace SimoshStore;

public interface IAuthRepository
{
    public List<User> GetUsers();
    User? ValidateUser(string email, string password);
    public List<Role> GetRoles();
}
