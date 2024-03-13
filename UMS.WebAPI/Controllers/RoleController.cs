using Microsoft.AspNetCore.Mvc;
using UMS.Common;
using UMS.Core.DTO;
using UMS.Core.IService;
using UMS.WebAPI.Models;

namespace UMS.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }
        [HttpGet]
        public async Task<ResultModel<RoleDTO[]>> GetRolesAsync()
        {
            var result = await _roleService.GetAllAsync();
            if (result.Any())
            {
                return ResultModel<RoleDTO[]>.Success(result, "成功啦");
            }
            else
            {
                return ResultModel<RoleDTO[]>.Error("没有找到数据");
            }
        }
        [HttpGet]
        public async Task<ResultModel<RoleDTO>> GetRoleAsync(long id)
        {
            var result = await _roleService.GetByIdAsync(id);
            if (result != null)
            {
                return ResultModel<RoleDTO>.Success(result, "成功啦");
            }
            else
            {
                return ResultModel<RoleDTO>.Error("没有找到该条数据");
            }
        }
        [HttpPost]
        public async Task<ResultModel<long>> AddRoleAsync(RoleAddPost model)
        {
            if (ModelState.IsValid)
            {
                RoleDTO roleDTO = new RoleDTO()
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsEnabled = model.IsEnabled
                };
                var result = await _roleService.AddAsync(roleDTO);

                if (result > 0)
                {
                    if (model.MenuIds.Any())
                    {
                        var menuIds = await _roleService.AddRoleMenusAsync(result, model.MenuIds);
                        if (menuIds)
                        {
                            return ResultModel<long>.Success(result, "成功啦");
                        }
                        return ResultModel<long>.Error("角色菜单添加失败");
                    }
                    return ResultModel<long>.Success(result, "成功啦");
                }
                return ResultModel<long>.Error("添加失败");
            }
            else
            {
                return ResultModel<long>.Error("模型校验失败");
            }
        }
        [HttpDelete]
        public async Task<ResultModel<bool>> DeleteRoleAsync(long id)
        {
            var result = await _roleService.DeleteAsync(id);
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
        public async Task<ResultModel<bool>> UpdateRoleAsync(RoleUpdatePost model)
        {
            if (ModelState.IsValid)
            {
                RoleDTO roleDTO = new RoleDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    IsEnabled = model.IsEnabled
                };
                var result = await _roleService.UpdateAsync(roleDTO);
                if (result)
                {
                    if (model.MenuIds.Any())
                    {
                        var menuIds = await _roleService.UpdateRoleMenusAsync(model.Id, model.MenuIds);
                        if (menuIds)
                        {
                            return ResultModel<bool>.Success(result, "成功啦");
                        }
                        return ResultModel<bool>.Error("角色菜单添加失败");
                    }
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
        public async Task<ResultModel<bool>> EnabledRoleAsync(RoleEnabledPut model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.UpdateAsync(model.Id, model.IsEnabled);
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
