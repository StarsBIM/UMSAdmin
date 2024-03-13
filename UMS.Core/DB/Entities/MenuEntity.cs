using UMS.Core.Enum;

namespace UMS.Core.DB.Entities
{
    /// <summary>
    /// 菜单实体
    /// </summary>
    public class MenuEntity : BaseEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单请求的Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public long? PaterId { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public virtual MenuEntity Pater { get; set; }
        /// <summary>
        /// 菜单的图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 菜单的类型
        /// </summary>
        public MenuType Type { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 具有这个菜单的角色
        /// </summary>
        public virtual ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
        public virtual ICollection<MenuEntity> Children { get; set; } = new List<MenuEntity>();
    }
}
