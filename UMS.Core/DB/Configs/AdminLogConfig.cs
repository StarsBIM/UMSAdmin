using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UMS.Core.DB.Entities;

namespace UMS.Core.DB.Configs
{
    public class AdminLogConfig : IEntityTypeConfiguration<AdminLogEntity>
    {
        public void Configure(EntityTypeBuilder<AdminLogEntity> builder)
        {
            builder.ToTable("T_AdminLogs");
            builder.Property(e => e.Message).IsRequired();
        }
    }
}
