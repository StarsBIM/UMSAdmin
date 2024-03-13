using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UMS.Core.DB;
using UMS.Core.DB.Entities;
using UMS.Core.DTO;
using UMS.Core.IService;

namespace UMS.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public RoleService(DBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        public async Task<RoleDTO[]> GetAllAsync()
        {
            BaseService<RoleEntity> service = new BaseService<RoleEntity>(_dbContext);
            var roleDtoArr = await service.GetAll().AsNoTracking().Include(e => e.Menus).ToArrayAsync();
            return _mapper.Map<RoleDTO[]>(roleDtoArr);
        }
        public async Task<RoleDTO> GetByIdAsync(long id)
        {
            BaseService<RoleEntity> service = new BaseService<RoleEntity>(_dbContext);
            var role = await service.GetAll().AsNoTracking().Where(e => e.Id == id).Include(e => e.Menus).SingleOrDefaultAsync();
            return role == null ? null : _mapper.Map<RoleDTO>(role);
        }
        public async Task<long> AddAsync(RoleDTO role)
        {
            RoleEntity roleEntity = new RoleEntity()
            {
                Name = role.Name,
                Description = role.Description,
                IsEnabled = role.IsEnabled
            };
            await _dbContext.Roles.AddAsync(roleEntity);
            return await _dbContext.SaveChangesAsync() > 0 ? roleEntity.Id : -1;
        }
        public async Task<bool> AddRoleMenusAsync(long roleId, long[] menuIds)
        {
            BaseService<RoleEntity> service = new BaseService<RoleEntity>(_dbContext);
            var role = await service.GetByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }
            BaseService<MenuEntity> menuService = new BaseService<MenuEntity>(_dbContext);
            var menus = await (from m in menuService.GetAll() where menuIds.Contains(m.Id) select m).ToListAsync();
            foreach (var item in menus)
            {
                role.Menus.Add(item);
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            BaseService<RoleEntity> service = new BaseService<RoleEntity>(_dbContext);
            return await service.MarkDeletedAsync(id);
        }
        public async Task<bool> UpdateAsync(RoleDTO role)
        {
            BaseService<RoleEntity> service = new BaseService<RoleEntity>(_dbContext);
            var roleEntity = await service.GetByIdAsync(role.Id);
            if (roleEntity == null)
            {
                return false;
            }
            if (roleEntity.Name == role.Name && roleEntity.Description == role.Description && roleEntity.IsEnabled == role.IsEnabled)
            {
                return true;
            }
            roleEntity.Name = role.Name;
            roleEntity.Description = role.Description;
            roleEntity.IsEnabled = role.IsEnabled;
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateRoleMenusAsync(long roleId, long[] menuIds)
        {
            BaseService<RoleEntity> service = new BaseService<RoleEntity>(_dbContext);
            var role = await service.GetByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }
            if (role.Menus.Count > 0)
            {
                role.Menus.Clear();
            }
            BaseService<MenuEntity> menuService = new BaseService<MenuEntity>(_dbContext);
            var menus = await (from m in menuService.GetAll() where menuIds.Contains(m.Id) select m).ToListAsync();
            foreach (var item in menus)
            {
                role.Menus.Add(item);
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(long id, bool isEnabled)
        {
            BaseService<RoleEntity> service = new BaseService<RoleEntity>(_dbContext);
            var entity = await service.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            entity.IsEnabled = isEnabled;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
