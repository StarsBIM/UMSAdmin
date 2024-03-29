﻿namespace UMS.Core.DTO
{
    public class RoleDTO : BaseDTO
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
        public long[] MenuIds { get; set; }
    }
}
