namespace UMS.Core.DTO
{
    public class UserDTO : BaseDTO
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int? LoginErrorTimes { get; set; }
        /// <summary>
        /// 最后登录错误时间
        /// </summary>
        public DateTime? LastLoginErrorDateTime { get; set; }
        /// <summary>
        /// 所在的城市
        /// </summary>
        public string[] City { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public long[] RoleIds { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
