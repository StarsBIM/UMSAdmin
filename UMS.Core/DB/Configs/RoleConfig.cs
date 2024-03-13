using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMS.Core.DB.Entities;

namespace UMS.Core.DB.Configs
{
    public class RoleConfig : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("T_Roles");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(1024);
            builder.HasMany(r => r.Menus).WithMany(p => p.Roles).UsingEntity(m => m.ToTable("T_RoleMenus"));
        }
    }
}
