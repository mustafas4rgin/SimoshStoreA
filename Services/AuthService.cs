using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace SimoshStore
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;
            _authRepository = authRepository;
        }

        // Kullanıcıyı doğrulama
        public User ValidatedUser(LoginViewModel loginRequest)
        {
            var user = _authRepository.ValidateUser(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                // Kullanıcı bulunamadığında hata mesajı dönebilirsiniz
                throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre");
            }
            return user;
        }

        // JWT Token üretme
        public string GenerateToken(User user)
        {
            var secretKey = _configuration["Jwt:Key"];
            var audience = _configuration["Jwt:Audience"];
            var issuer = _configuration["Jwt:Issuer"];

            // Anahtarın boyutunu 256 bit (32 byte) yapalım
            if (secretKey.Length < 32)
            {
                // Anahtar uzunluğunu 256 bit yapıyoruz (32 byte)
                secretKey = secretKey.PadRight(32, 'X'); // 32 byte (256 bit) uzunluğunda olacak
            }

            var role = _authRepository.GetRoles().Where(r => r.Id == user.RoleId).FirstOrDefault();

            var claims = new List<Claim>
            {
                new Claim("name", user.Email), // "name" claim, URL olmadan
                new Claim("nameidentifier", user.Id.ToString()), // "nameidentifier" claim
                new Claim("role", role.Name) // "role" claim, URL olmadan
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15), // Token süresi
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // JWT Token doğrulama
        public ClaimsPrincipal ValidateToken(string token)
        {
            var secretKey = _configuration["Jwt:Key"];
            var audience = _configuration["Jwt:Audience"];
            var issuer = _configuration["Jwt:Issuer"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out var validatedToken);

                return principal;
            }
            catch (Exception ex)
            {
                // Hata durumunda daha ayrıntılı bir hata mesajı dönebilirsiniz
                Console.WriteLine($"Token doğrulama hatası: {ex.Message}");
                return null;
            }
        }
    }
}
