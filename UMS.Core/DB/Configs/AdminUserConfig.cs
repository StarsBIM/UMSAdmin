using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMS.Core.DB.Entities;

namespace UMS.Core.DB.Configs
{
    public class AdminUserConfig : IEntityTypeConfiguration<AdminUserEntity>
    {
        public void Configure(EntityTypeBuilder<AdminUserEntity> builder)
        {
            builder.ToTable("T_AdminUsers");
            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.City).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.Description).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.Email).HasMaxLength(30).IsRequired().IsUnicode(false);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired().IsUnicode(false);
            builder.Property(e => e.PasswordSalt).HasMaxLength(50).IsRequired().IsUnicode(false);
            builder.Property(e => e.PasswordHash).HasMaxLength(100).IsRequired().IsUnicode(false);
            builder.HasMany(u => u.Roles).WithMany(r => r.AdminUsers).UsingEntity(m => m.ToTable("T_AdminUserRoles"));
        }
    }
}
