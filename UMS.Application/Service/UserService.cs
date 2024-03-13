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
    public class UserService : IUserService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(DBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<UserDTO[]> GetAllAsync()
        {
            BaseService<UserEntity> service = new BaseService<UserEntity>(_dbContext);
            var userDtoArr = await service.GetAll().AsNoTracking().ToArrayAsync();
            return _mapper.Map<UserDTO[]>(userDtoArr);
        }
        public async Task<UserDTO> GetByIdAsync(long id)
        {
            BaseService<UserEntity> service = new BaseService<UserEntity>(_dbContext);
            var user = await service.GetAll().AsNoTracking().Where(e => e.Id == id).Include(e => e.Roles).FirstOrDefaultAsync();
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }
        public async Task<long> AddAsync(UserUpdateDTO user)
        {
            UserEntity userEntity = new UserEntity();
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
            await _dbContext.Users.AddAsync(userEntity);
            return await _dbContext.SaveChangesAsync() > 0 ? userEntity.Id : -1;
        }
        public async Task<bool> AddUserRolesAsync(long userId, long[] roleIds)
        {
            BaseService<UserEntity> userService = new BaseService<UserEntity>(_dbContext);
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
            BaseService<UserEntity> service = new BaseService<UserEntity>(_dbContext);
            return await service.MarkDeletedAsync(id);
        }

        public async Task<bool> UpdateAsync(UserUpdateDTO user)
        {
            BaseService<UserEntity> service = new BaseService<UserEntity>(_dbContext);
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
            await _dbContext.Users.AddAsync(userEntity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(long id, bool isEnable)
        {
            BaseService<UserEntity> service = new BaseService<UserEntity>(_dbContext);
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
