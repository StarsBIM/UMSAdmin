using UMS.Core.DTO;

namespace UMS.Core.IService
{
    public interface IUserService : IServiceSupport
    {
        /// <summary>
        /// 获得所有的用户
        /// </summary>
        /// <returns></returns>
        Task<UserDTO[]> GetAllAsync();
        /// <summary>
        /// 根据Id获得用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDTO> GetByIdAsync(long id);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<long> AddAsync(UserUpdateDTO user);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserUpdateDTO user);
        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(long id, bool isEnabled);
        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> AddUserRolesAsync(long userId, long[] roleIds);

    }
}
