using System;

namespace UMS.Core.DB.Entities
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否软删除，默认为false
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
