using UMS.Core.DTO;

namespace UMS.Core.IService
{
    public interface IAdminUserService : IServiceSupport
    {
        /// <summary>
        /// 获得所有的管理员用户
        /// </summary>
        /// <returns></returns>
        Task<AdminUserDTO[]> GetAllAsync();
        /// <summary>
        /// 根据Id获得管理员用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AdminUserDTO> GetByIdAsync(long id);
        /// <summary>
        /// 添加管理员用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<long> AddAsync(AdminUserUpdateDTO adminUser);
        /// <summary>
        /// 删除管理员用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
        /// <summary>
        /// 编辑管理员用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(AdminUserUpdateDTO adminUser);
        /// <summary>
        /// 启用管理员用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(long id, bool isEnabled);
        /// <summary>
        /// 添加管理员用户角色
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> AddAdminUserRolesAsync(long adminUserId, long[] roleIds);
        /// <summary>
        /// 添加管理员用户角色
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> UpdateAdminUserRolesAsync(long adminUserId, long[] roleIds);
    }
}
