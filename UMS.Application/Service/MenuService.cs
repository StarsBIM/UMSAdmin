using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UMS.Core.DB;
using UMS.Core.DB.Entities;
using UMS.Core.DTO;
using UMS.Core.Enum;
using UMS.Core.IService;

namespace UMS.Application.Service
{
    public class MenuService : IMenuService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public MenuService(DBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        public async Task<MenuDTO[]> GetAllAsync()
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            var menuArr = await service.GetAll()
                .Include(e => e.Pater)
                .OrderBy(o => o.Sort)
                .ToArrayAsync();
            return _mapper.Map<MenuDTO[]>(menuArr);
        }

        public async Task<MenuDTO> GetByIdAsync(long id)
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            var menu = await service.GetByIdAsync(id);
            return menu == null ? null : _mapper.Map<MenuDTO>(menu);
        }
        public async Task<MenuDTO[]> GetMenusAsync()
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            var menuArr = await service.GetAll()
                .Where(item => item.Type != MenuType.Btn && item.IsEnabled)
                .OrderBy(o => o.Sort)
                .ToArrayAsync();
            return _mapper.Map<MenuDTO[]>(menuArr.Where(item => item.PaterId == null).ToArray());
        }
        public async Task<MenuDTO[]> GetDirsAsync()
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            var menuArr = await service.GetAll()
                .AsNoTracking()
                .Where(item => item.Type != MenuType.Btn)
                .ToArrayAsync();
            return _mapper.Map<MenuDTO[]>(menuArr);
        }
        public async Task<MenuDTO[]> GetMenuBtnsAsync(long id)
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            var menuArr = await service.GetAll()
             .AsNoTracking()
             .Where(item => item.Type == MenuType.Btn && item.PaterId == id)
             .ToArrayAsync();
            return _mapper.Map<MenuDTO[]>(menuArr);
        }
        public async Task<long> AddAsync(MenuDTO menu)
        {
            MenuEntity entity = new MenuEntity()
            {
                Name = menu.Name,
                Url = menu.Url,
                Icon = menu.Icon,
                PaterId = menu.PaterId,
                Type = menu.Type,
                Description = menu.Description,
                Sort = menu.Sort,
                IsEnabled = menu.IsEnabled
            };
            await _dbContext.Menus.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0 ? entity.Id : -1;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            return await service.MarkDeletedAsync(id);
        }
        public async Task<bool> UpdateAsync(MenuDTO menu)
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            var entity = await service.GetByIdAsync(menu.Id);
            if (entity == null)
            {
                return false;
            }
            entity.Name = menu.Name;
            entity.Url = menu.Url ?? null;
            entity.Icon = menu.Icon;
            entity.PaterId = menu.PaterId;
            entity.Type = menu.Type;
            entity.Description = menu.Description ?? null;
            entity.IsEnabled = menu.IsEnabled;
            entity.Sort = menu.Sort;
            return await _dbContext.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateAsync(long id, bool isEnable)
        {
            BaseService<MenuEntity> service = new BaseService<MenuEntity>(_dbContext);
            var entity = await service.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            entity.IsEnabled = isEnable;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<MenuDTO[]> GetMenuByRoleIdsAsync(long[] roleIds)
        {
            try
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(_dbContext);
                var menus = new List<MenuEntity>();

                foreach (var roleId in roleIds)
                {
                    var role = await baseService.GetAll().AsNoTracking().Where(e => e.Id == roleId).Include(e => e.Menus).SingleOrDefaultAsync();
                    foreach (var menu in role.Menus)
                    {
                        if (menu.Type == MenuType.Btn)
                        {
                            break;
                        }
                        menus.Add(menu);
                    }
                }

                return _mapper.Map<MenuDTO[]>(menus);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                return null;
            }

        }
    }
}
