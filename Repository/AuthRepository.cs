using SimoshStore;
using System.Linq;

namespace SimoshStore
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuthRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // Kullanıcı doğrulama
        public User? ValidateUser(string email, string password)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null; // Kullanıcı bulunamadı
            }

            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null; // Şifre yanlış
            }

            return user; // Kullanıcı doğrulandı
        }

        // Tüm kullanıcıları getir
        public List<User> GetUsers()
        {
            return _appDbContext.Users.ToList();
        }
        public List<Role> GetRoles()
        {
            return _appDbContext.Roles.ToList();
        }
    }
}
