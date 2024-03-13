namespace UMS.Core.DTO
{
    public class UserUpdateDTO : BaseDTO
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
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// 所在的城市Id
        /// </summary>
        public string[] City { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
