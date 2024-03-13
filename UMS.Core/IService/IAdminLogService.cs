using UMS.Core.DTO;

namespace UMS.Core.IService
{
    public interface IAdminLogService
    {
        /// <summary>
        /// 获得所有的管理员日志
        /// </summary>
        /// <returns></returns>
        AdminLogDTO[] GetAll();
        /// <summary>
        /// 根据Id获得管理员日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdminLogDTO GetById(long id);
        /// <summary>
        /// 添加管理员日志
        /// </summary>
        /// <param name="adminLog"></param>
        /// <returns></returns>
        long Add(AdminLogDTO adminLog);
        /// <summary>
        /// 删除管理员日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(long id);
        /// <summary>
        /// 编辑管理员日志
        /// </summary>
        /// <param name="adminLog"></param>
        /// <returns></returns>
        bool Update(AdminLogDTO adminLog);
    }
}
