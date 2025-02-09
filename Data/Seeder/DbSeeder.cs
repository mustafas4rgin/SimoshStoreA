using SimoshStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class DbSeeder
{
    public static void SeedData(AppDbContext context)
    {
        // Veritabanında rol var mı kontrol et, yoksa oluştur
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(GetRoles());
            context.SaveChanges();
        }

        // Veritabanında kullanıcı var mı kontrol et, yoksa oluştur
        if (!context.Users.Any())
        {
            context.Users.AddRange(GetUsers(context));
            context.SaveChanges();
        }
    }

    private static List<Role> GetRoles()
    {
        return new List<Role>
        {
            new Role { Id = 2,Name = "Admin" },
            new Role { Id = 1,Name = "User" }
        };
    }

    private static List<User> GetUsers(AppDbContext context)
    {
        var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
        var userRole = context.Roles.FirstOrDefault(r => r.Name == "User");

        byte[] passwordHash1, passwordSalt1;
        byte[] passwordHash2, passwordSalt2;
        byte[] passwordHash3, passwordSalt3;
        HashingHelper.CreatePasswordHash("1234", out passwordHash1, out passwordSalt1);
        HashingHelper.CreatePasswordHash("1234", out passwordHash2, out passwordSalt2);
        HashingHelper.CreatePasswordHash("1234", out passwordHash3, out passwordSalt3);

        return new List<User>
        {
            new User
            {
                Email = "admin@example.com",
                PasswordHash = passwordHash1,
                PasswordSalt = passwordSalt1,
                FirstName = "Admin",
                LastName = "User",
                Phone = "1234567890",
                RoleId = 2,
                UserRoles = new List<UserRole>
                {
                    new UserRole { Role = adminRole }
                }
            },
            new User
            {
                Email = "user1@example.com",
                PasswordHash = passwordHash2,
                PasswordSalt = passwordSalt2,
                FirstName = "Regular",
                LastName = "User1",
                Phone = "9876543210",
                UserRoles = new List<UserRole>
                {
                    new UserRole { Role = userRole }
                }
            },
            new User
            {
                Email = "user2@example.com",
                PasswordHash = passwordHash3,
                PasswordSalt = passwordSalt3,
                FirstName = "Regular",
                LastName = "User2",
                Phone = "5556667777",
                UserRoles = new List<UserRole>
                {
                    new UserRole { Role = userRole }
                }
            }
        };
    }
}