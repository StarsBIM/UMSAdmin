using UMS.Core.Enum;

namespace UMS.Core.DTO
{
    public class MenuDTO : BaseDTO
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
        /// 菜单的图标Id
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public long? PaterId { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public string PaterName { get; set; }
        /// <summary>
        /// 子项
        /// </summary>
        public MenuDTO[] Children { get; set; }
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
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
