using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UMS.Core.DB.Entities;

namespace UMS.Core.DB
{
    public class DBContext : DbContext
    {
        //声明Log4net对象，建议一个类声明一个ILog对象
        //private static readonly ILog log = LogManager.GetLogger(typeof(DBContext));
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    //将EF生成的sql语句记录在日志里面
        //    optionsBuilder.UseSqlServer(sql =>
        //    {
        //        log.DebugFormat("EF开始执行sql语句{0}", sql);
        //    });
        //}

        public DBContext(DbContextOptions options) : base(options)
        {
            //这句加上后，EF Core会根据实体模型自动生成数据库
            //this.Database.EnsureCreatedAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<AdminLogEntity> AdminLogs { get; set; }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
