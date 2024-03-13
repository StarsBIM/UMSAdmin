using Microsoft.AspNetCore.Mvc;
using UMS.Common;
using UMS.Core.DTO;
using UMS.Core.IService;
using UMS.WebAPI.Models;

namespace UMS.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet]
        public async Task<ResultModel<UserDTO[]>> GetUsersAsync()
        {
            var result = await _userService.GetAllAsync();
            if (result.Any())
            {
                return ResultModel<UserDTO[]>.Success(result, "成功啦");
            }
            else
            {
                return ResultModel<UserDTO[]>.Error("没有找到数据");
            }
        }
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetUserAsync(long id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result != null)
            {
                return ResultModel<UserDTO>.Success(result, "成功啦");
            }
            else
            {
                return ResultModel<UserDTO>.Error("没有找到该条数据");
            }
        }
        [HttpPost]
        public async Task<ResultModel<long>> AddUserAsync(UserAddPost model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return ResultModel<long>.Error("确认密码与密码不一致！");
                }
                UserUpdateDTO userDTO = new UserUpdateDTO()
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
                var userId = await _userService.AddAsync(userDTO);
                if (userId <= 0)
                {
                    return ResultModel<long>.Error("添加失败");
                }
                var role = await _userService.AddUserRolesAsync(userId, model.RoleIds);
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
        public async Task<ResultModel<bool>> DeleteUserAsync(long id)
        {
            var result = await _userService.DeleteAsync(id);
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
        public async Task<ResultModel<bool>> UpdateUserAsync(UserUpdatePost model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return ResultModel<bool>.Error("确认密码与密码不一致！");
                }
                UserUpdateDTO userDTO = new UserUpdateDTO()
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
                var result = await _userService.UpdateAsync(userDTO);
                if (result)
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
        public async Task<ResultModel<bool>> EnabledUserAsync(UserEnabledPut model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateAsync(model.Id, model.IsEnabled);
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
