using System;

namespace UMS.Core.DTO
{
    public class BaseDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDataTime { get; set; }
    }
}
