
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimoshStore;

namespace App.Data.Entities;

public class UserEntity : EntityBase, IHasEnabled
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = default!;
    public int RoleId { get; set; } = 3;
    public bool Enabled { get; set; } = true;
    public string Address{ get; set; } = string.Empty;
    public bool HasSellerRequest { get; set; } = false;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public string ResetToken { get; set; } = null!;
    public DateTime ResetTokenExpires { get; set; }

    // Navigation properties
    public RoleEntity Role { get; set; } = null!;
}

internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(256);
        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.RoleId).IsRequired();
        builder.Property(e => e.Enabled).IsRequired().HasDefaultValue(true);
        builder.Property(e => e.CreatedAt).IsRequired();

        builder.HasOne(d => d.Role)
            .WithMany()
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

        new UserEntitySeed().SeedData(builder);
    }
}

internal class UserEntitySeed : IEntityTypeSeed<UserEntity>
{
    public void SeedData(EntityTypeBuilder<UserEntity> builder)
    {
        HashingHelper.CreatePasswordHash("1234",out var passwordHash, out var passwordSalt);
        builder.HasData(
            new UserEntity() { Id = 1,ResetToken =string.Empty,ResetTokenExpires=DateTime.MinValue, Address ="Antalya, Muratpaşa" ,FirstName = "admin", LastName = "admin", Email = "mustafas4rgin@gmail.com", Enabled = true, RoleId = 1,  Phone ="05341233212",PasswordHash = passwordHash, PasswordSalt = passwordSalt, CreatedAt = new DateTime(2025, 2, 23) },
            new UserEntity() { Id = 2, ResetToken =string.Empty,ResetTokenExpires=DateTime.MinValue,Address ="İstanbul, Kadıköy", FirstName = "seller", LastName = "seller", Email = "seller@siliconmade.com", Enabled = true, RoleId = 2, Phone="05555555555",PasswordHash = passwordHash, PasswordSalt = passwordSalt, CreatedAt = new DateTime(2025, 2, 23) },
            new UserEntity() { Id = 3, ResetToken =string.Empty,ResetTokenExpires=DateTime.MinValue,Address ="Muğla, marmaris", FirstName = "buyer", LastName = "buyer", Email = "buyer@siliconmade.com", Enabled = true, RoleId = 3,  Phone = "05333333333",PasswordHash = passwordHash, PasswordSalt = passwordSalt,CreatedAt = new DateTime(2025, 2, 23) }
        );
    }
}