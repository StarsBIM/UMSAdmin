using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UMS.Common;
using UMS.Core.DB;
using UMS.Core.DB.Entities;
using UMS.Core.DTO;
using UMS.Core.IService;

namespace UMS.Application.Service
{
    public class AdminUserService : IAdminUserService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public AdminUserService(DBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<AdminUserDTO[]> GetAllAsync()
        {
            BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(_dbContext);
            var userDtoArr = await service.GetAll().AsNoTracking().Include(e => e.Roles).ToArrayAsync();
            return _mapper.Map<AdminUserDTO[]>(userDtoArr);
        }
        public async Task<AdminUserDTO> GetByIdAsync(long id)
        {
            BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(_dbContext);
            var user = await service.GetAll().AsNoTracking().Where(e => e.Id == id).Include(e => e.Roles).SingleOrDefaultAsync();
            return user == null ? null : _mapper.Map<AdminUserDTO>(user);
        }
        public async Task<long> AddAsync(AdminUserUpdateDTO admin)
        {
            AdminUserEntity adminUser = new AdminUserEntity();
            adminUser.Name = admin.Name;
            adminUser.Email = admin.Email;
            adminUser.PhoneNumber = admin.PhoneNumber;
            string salt = CommonHelper.CreateVerifyCode(5);
            string pwsHash = CommonHelper.CalcMD5(salt + admin.Password);
            adminUser.PasswordSalt = salt;
            adminUser.PasswordHash = pwsHash;
            adminUser.Description = admin.Description;
            adminUser.City = JsonConvert.SerializeObject(admin.City);
            adminUser.IsEnabled = admin.IsEnabled;
            await _dbContext.AdminUsers.AddAsync(adminUser);
            return await _dbContext.SaveChangesAsync() > 0 ? adminUser.Id : -1;
        }
        public async Task<bool> AddAdminUserRolesAsync(long userId, long[] roleIds)
        {
            BaseService<AdminUserEntity> userService = new BaseService<AdminUserEntity>(_dbContext);
            var user = await userService.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            BaseService<RoleEntity> roleService = new BaseService<RoleEntity>(_dbContext);
            var roles = await (from m in roleService.GetAll() where roleIds.Contains(m.Id) select m).ToListAsync();
            foreach (var role in roles)
            {
                user.Roles.Add(role);
            }
            return await _dbContext.SaveChangesAsync() > 0;

        }
        public async Task<bool> DeleteAsync(long id)
        {
            BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(_dbContext);
            return await service.MarkDeletedAsync(id);
        }
        public async Task<bool> UpdateAsync(AdminUserUpdateDTO user)
        {
            BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(_dbContext);
            var userEntity = await service.GetByIdAsync(user.Id);
            if (userEntity == null)
            {
                return false;
            }
            userEntity.Name = user.Name;
            userEntity.Email = user.Email;
            userEntity.PhoneNumber = user.PhoneNumber;
            string salt = CommonHelper.CreateVerifyCode(5);
            string pwsHash = CommonHelper.CalcMD5(salt + user.Password);
            userEntity.PasswordSalt = salt;
            userEntity.PasswordHash = pwsHash;
            userEntity.Description = user.Description;
            userEntity.City = JsonConvert.SerializeObject(user.City);
            userEntity.IsEnabled = user.IsEnabled;
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAdminUserRolesAsync(long adminUserId, long[] roleIds)
        {
            BaseService<AdminUserEntity> userService = new BaseService<AdminUserEntity>(_dbContext);
            var user = await userService.GetByIdAsync(adminUserId);
            if (user == null)
            {
                return false;
            }
            BaseService<RoleEntity> roleService = new BaseService<RoleEntity>(_dbContext);
            var roles = await (from m in roleService.GetAll() where roleIds.Contains(m.Id) select m).ToArrayAsync();
            if (roles.Length > 0)
            {
                user.Roles.Clear();
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                }
            }
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(long id, bool isEnable)
        {
            BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(_dbContext);
            var entity = await service.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            entity.IsEnabled = isEnable;
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
