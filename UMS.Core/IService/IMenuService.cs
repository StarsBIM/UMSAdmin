using UMS.Core.DTO;

namespace UMS.Core.IService
{
    public interface IMenuService : IServiceSupport
    {
        /// <summary>
        /// 获得所有的菜单
        /// </summary>
        /// <returns></returns>
        Task<MenuDTO[]> GetAllAsync();
        /// <summary>
        /// 根据Id获得菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuDTO> GetByIdAsync(long id);
        /// <summary>
        /// 获取不包括按钮的菜单
        /// </summary>
        /// <returns></returns>
        Task<MenuDTO[]> GetMenusAsync();
        /// <summary>
        /// 获取所有目录
        /// </summary>
        /// <returns></returns>
        Task<MenuDTO[]> GetDirsAsync();
        /// <summary>
        /// 根据菜单id获取所包含的按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuDTO[]> GetMenuBtnsAsync(long id);
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<long> AddAsync(MenuDTO menu);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(MenuDTO menu);
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(long id, bool isEnable);
        Task<MenuDTO[]> GetMenuByRoleIdsAsync(long[] roleIds);
    }
}
