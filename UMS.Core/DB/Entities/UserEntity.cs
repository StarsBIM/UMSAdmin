namespace UMS.Core.DB.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 通过MD5与密码盐加密后的密码
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// 密码盐
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int? LoginErrorTimes { get; set; }
        /// <summary>
        /// 最后登录错误时间
        /// </summary>
        public DateTime? LastLoginErrorDateTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 用户具有的角色信息
        /// </summary>
        public virtual ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
