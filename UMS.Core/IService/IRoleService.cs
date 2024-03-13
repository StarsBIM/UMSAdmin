using UMS.Core.DTO;

namespace UMS.Core.IService
{
    public interface IRoleService : IServiceSupport
    {
        /// <summary>
        /// 获得所有的角色
        /// </summary>
        /// <returns></returns>
        Task<RoleDTO[]> GetAllAsync();
        /// <summary>
        /// 根据Id获得角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RoleDTO> GetByIdAsync(long id);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<long> AddAsync(RoleDTO role);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(RoleDTO role);
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(long id, bool isEnable);
        Task<bool> UpdateRoleMenusAsync(long roleId, long[] menuIds);
        /// <summary>
        /// 添加角色菜单
        /// </summary>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        Task<bool> AddRoleMenusAsync(long roleId, long[] menuIds);
    }
}
