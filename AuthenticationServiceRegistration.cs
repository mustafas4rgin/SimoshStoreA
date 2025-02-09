using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimoshStore
{
    public static class AuthenticationServiceRegistration
    {
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            services.Configure<JwtSettings>(jwtSettings);

            // JWT doğrulama işlemleri
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = GetSymmetricSecurityKey(jwtSettings["Key"]),
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                    };
                });

            return services;
        }

        public static IServiceCollection AddingAuthorization(this IServiceCollection services)
        {
            // Yetkilendirme politikalarını tanımla
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });

            return services;
        }

        // Anahtarın boyutunu kontrol eden yardımcı metod
        private static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            // Eğer anahtarın uzunluğu 128 bitten küçükse, uygun boyuta getirin
            if (key.Length < 16)
            {
                key = key.PadRight(16, 'X'); // Anahtar boyutu yetersizse, 16 byte (128 bit) yapmak için 'X' ile doldur
            }

            // Anahtarı byte dizisine çevir ve SymmetricSecurityKey oluştur
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}
