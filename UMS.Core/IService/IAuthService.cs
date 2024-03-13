using UMS.Core.DTO;

namespace UMS.Core.IService
{
    public interface IAuthService : IServiceSupport
    {
        /// <summary>
        /// 管理员用户登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AdminUserDTO> LoginAsync(string name, string password);
    }
}
