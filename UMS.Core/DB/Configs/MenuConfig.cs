using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMS.Core.DB.Entities;

namespace UMS.Core.DB.Configs
{
    public class MenuConfig : IEntityTypeConfiguration<MenuEntity>
    {
        public void Configure(EntityTypeBuilder<MenuEntity> builder)
        {
            builder.ToTable("T_Menus");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Url).IsRequired(false).HasMaxLength(50);
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Icon).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(50);
            builder.Property(p => p.Sort).IsRequired();
        }
    }
}
