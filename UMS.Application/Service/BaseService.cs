using Microsoft.EntityFrameworkCore;
using UMS.Core.DB;
using UMS.Core.DB.Entities;

namespace UMS.Application.Service
{
    public class BaseService<T> where T : BaseEntity
    {
        private readonly DBContext _dbContext;
        public BaseService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 获取所有没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            //此处为延迟加载，只用遍历结果集时候才执行sql语句，例如ToList或者ToArray操作
            return _dbContext.Set<T>().Where(e => e.IsDeleted == false);
        }
        /// <summary>
        /// 获取总数据条数
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetTotalCountAsync()
        {
            return await GetAll().LongCountAsync();
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageData(int startIndex, int count)
        {
            //按照创建时间进行排序，skip是返回跳过n条数据后的剩余数据，Take是从当前序列的开头取n条数据
            return GetAll().OrderBy(e => e.CreateDateTime).Skip(startIndex).Take(count);
        }
        /// <summary>
        /// 按照id查找数据，找不到就返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(long id)
        {
            return await GetAll().Where(m => m.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> MarkDeletedAsync(long id)
        {
            var data = await GetByIdAsync(id);
            if (data == null)
            {
                return false;
            }
            data.IsDeleted = true;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
