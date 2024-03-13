namespace UMS.Core.DB.Entities
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class RoleEntity : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 角色拥有的权限
        /// </summary>
        public virtual ICollection<MenuEntity> Menus { get; set; } = new List<MenuEntity>();
        /// <summary>
        /// 具有这个角色信息的用户
        /// </summary>
        public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
        /// <summary>
        /// 具有这个角色信息的管理员
        /// </summary>
        public virtual ICollection<AdminUserEntity> AdminUsers { get; set; } = new List<AdminUserEntity>();
    }
}
