using Microsoft.AspNetCore.Mvc;
using UMS.Common;
using UMS.Core.DTO;
using UMS.Core.IService;
using UMS.WebAPI.Models;

namespace UMS.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;
        public AdminUserController(IAdminUserService adminUserService)
        {
            this._adminUserService = adminUserService;
        }
        [HttpGet]
        public async Task<ResultModel<AdminUserDTO[]>> GetAdminUsersAsync()
        {
            var result = await _adminUserService.GetAllAsync();
            if (result.Any())
            {
                return ResultModel<AdminUserDTO[]>.Success(result, "成功啦");
            }
            else
            {
                return ResultModel<AdminUserDTO[]>.Error("没有找到数据");
            }
        }
        [HttpGet]
        public async Task<ResultModel<AdminUserDTO>> GetAdminUserAsync(long id)
        {
            var result = await _adminUserService.GetByIdAsync(id);
            if (result != null)
            {
                return ResultModel<AdminUserDTO>.Success(result, "成功啦");
            }
            else
            {
                return ResultModel<AdminUserDTO>.Error("没有找到该条数据");
            }
        }
        [HttpPost]
        public async Task<ResultModel<long>> AddAdminUserAsync(AdminUserAddPost model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return ResultModel<long>.Error("确认密码与密码不一致！");
                }
                AdminUserUpdateDTO adminUserDTO = new AdminUserUpdateDTO()
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    Description = model.Description,
                    City = model.City,

                    IsEnabled = model.IsEnabled
                };
                var userId = await _adminUserService.AddAsync(adminUserDTO);
                if (userId <= 0)
                {
                    return ResultModel<long>.Error("添加失败");
                }
                var role = await _adminUserService.AddAdminUserRolesAsync(userId, model.RoleIds);
                if (role)
                {
                    return ResultModel<long>.Success(userId, "成功啦");
                }
                return ResultModel<long>.Error("添加失败");
            }
            else
            {
                return ResultModel<long>.Error("模型校验失败");
            }
        }
        [HttpDelete]
        public async Task<ResultModel<bool>> DeleteAdminUserAsync(long id)
        {
            var result = await _adminUserService.DeleteAsync(id);
            if (result)
            {
                return ResultModel<bool>.Success(result);
            }
            else
            {
                return ResultModel<bool>.Error("删除失败");
            }
        }
        [HttpPut]
        public async Task<ResultModel<bool>> UpdateAdminUserAsync(AdminUserUpdatePost model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return ResultModel<bool>.Error("确认密码与密码不一致！");
                }
                AdminUserUpdateDTO adminUserDTO = new AdminUserUpdateDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    Description = model.Description,
                    City = model.City,
                    IsEnabled = model.IsEnabled
                };
                var result = await _adminUserService.UpdateAsync(adminUserDTO);
                if (!result)
                {
                    return ResultModel<bool>.Error("更新失败");
                }
                var role = await _adminUserService.UpdateAdminUserRolesAsync(model.Id, model.RoleIds);
                if (role)
                {
                    return ResultModel<bool>.Success(result, "成功啦");
                }
                return ResultModel<bool>.Error("更新失败");
            }
            else
            {
                return ResultModel<bool>.Error("模型校验失败");
            }
        }
        [HttpPut]
        public async Task<ResultModel<bool>> EnabledAdminUserAsync(AdminUserEnabledPut model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminUserService.UpdateAsync(model.Id, model.IsEnabled);
                if (result)
                {
                    return ResultModel<bool>.Success(result, "成功啦");
                }
                return ResultModel<bool>.Error(model.IsEnabled ? "启用失败" : "关闭失败");
            }
            return ResultModel<bool>.Error("模型校验失败");
        }
    }
}
