using System.ComponentModel.DataAnnotations;
using UMS.Core.Enum;

namespace UMS.WebAPI.Models
{
    public class MenuAddPost
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 菜单请求的Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 菜单的图标
        /// </summary>
        [Required]
        public string Icon { get; set; }
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public long? PaterId { get; set; }
        /// <summary>
        /// 菜单的类型
        /// </summary>
        [Required]
        public MenuType Type { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}
