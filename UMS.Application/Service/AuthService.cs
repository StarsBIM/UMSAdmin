using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UMS.Common;
using UMS.Core.DB;
using UMS.Core.DB.Entities;
using UMS.Core.DTO;
using UMS.Core.IService;

namespace UMS.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public AuthService(DBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        public async Task<AdminUserDTO> LoginAsync(string name, string password)
        {
            try
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(_dbContext);
                var admin = await service.GetAll().AsNoTracking().Where(e => e.Name == name).Include(e => e.Roles).FirstOrDefaultAsync();
                if (admin == null)
                {
                    return null;
                }
                var salt = admin.PasswordSalt;
                var newPasswordHash = CommonHelper.CalcMD5(password + salt);
                return newPasswordHash == admin.PasswordHash ? _mapper.Map<AdminUserDTO>(admin) : null;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                return null;
            }

        }
    }
}
