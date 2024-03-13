using Microsoft.AspNetCore.Mvc;
using UMS.Common;
using UMS.Core.DTO;
using UMS.Core.IService;
using UMS.WebAPI.Models;

namespace UMS.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            this._menuService = menuService;
        }
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<MenuDTO[]>> GetAllMenusAsync()
        {
            var result = await _menuService.GetAllAsync();
            if (result.Any())
            {
                return ResultModel<MenuDTO[]>.Success(result, "成功啦");
            }
            return ResultModel<MenuDTO[]>.Error("没有找到数据", 404);
        }
        /// <summary>
        /// 获取不包含按钮的所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<MenuDTO[]>> GetMenusAsync()
        {
            var result = await _menuService.GetMenusAsync();
            if (result.Any())
            {
                return ResultModel<MenuDTO[]>.Success(result, "成功啦");
            }
            return ResultModel<MenuDTO[]>.Error("没有找到数据", 404);
        }
        [HttpPost]
        public async Task<ResultModel<MenuDTO[]>> GetMenuByRoleIdsAsync(MenuGetByRoleIds model)
        {
            if (ModelState.IsValid)
            {
                var result = await _menuService.GetMenuByRoleIdsAsync(model.RoleIds);
                if (result.Any())
                {
                    return ResultModel<MenuDTO[]>.Success(result, "成功啦");
                }
                return ResultModel<MenuDTO[]>.Error("没有找到数据", 404);
            }
            return ResultModel<MenuDTO[]>.Error("模型校验失败");
        }
        /// <summary>
        /// 获取所有目录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<MenuDTO[]>> GetDirsAsync()
        {
            var result = await _menuService.GetDirsAsync();
            if (result.Any())
            {
                return ResultModel<MenuDTO[]>.Success(result, "成功啦");
            }
            return ResultModel<MenuDTO[]>.Error("没有找到数据", 404);
        }
        /// <summary>
        /// 根据菜单id查询所包含的按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<MenuDTO[]>> GetMenuBtnsAsync(long id)
        {
            var result = await _menuService.GetMenuBtnsAsync(id);
            if (result.Any())
            {
                return ResultModel<MenuDTO[]>.Success(result, "成功啦");
            }
            return ResultModel<MenuDTO[]>.Error("没有找到数据", 404);
        }
        /// <summary>
        /// 根据id查询菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<MenuDTO>> GetMenuAsync(long id)
        {
            var result = await _menuService.GetByIdAsync(id);
            if (result != null)
            {
                return ResultModel<MenuDTO>.Success(result, "成功啦");
            }
            return ResultModel<MenuDTO>.Error("没有找到该条数据", 404);
        }
        [HttpPost]
        public async Task<ResultModel<long>> AddMenuAsync(MenuAddPost model)
        {
            if (ModelState.IsValid)
            {
                MenuDTO menuDTO = new MenuDTO()
                {
                    Name = model.Name,
                    Url = model.Url ?? null,
                    Icon = model.Icon,
                    PaterId = model.PaterId == 0 ? null : model.PaterId,
                    Type = model.Type,
                    Description = model.Description ?? null,
                    IsEnabled = model.IsEnabled,
                    Sort = model.Sort,
                };
                var result = await _menuService.AddAsync(menuDTO);
                if (result > 0)
                {
                    return ResultModel<long>.Success(result, "成功啦");
                }
                return ResultModel<long>.Error("添加失败");
            }
            return ResultModel<long>.Error("模型校验失败");
        }
        [HttpDelete]
        public async Task<ResultModel<bool>> DeleteMenuAsync(long id)
        {
            if (id <= 0)
            {
                return ResultModel<bool>.Error($"参数错误:你的参数为{id},请检查");
            }
            var result = await _menuService.DeleteAsync(id);
            if (result)
            {
                return ResultModel<bool>.Success(result, "成功啦");
            }
            return ResultModel<bool>.Error("删除失败");
        }
        [HttpPut]
        public async Task<ResultModel<bool>> UpdateMenuAsync(MenuUpdatePost model)
        {
            if (ModelState.IsValid)
            {
                MenuDTO menuDTO = new MenuDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Url = model.Url ?? null,
                    Icon = model.Icon,
                    PaterId = model.PaterId == 0 ? null : model.PaterId,
                    Type = model.Type,
                    Description = model.Description ?? null,
                    IsEnabled = model.IsEnabled,
                    Sort = model.Sort,
                };
                var result = await _menuService.UpdateAsync(menuDTO);
                if (result)
                {
                    return ResultModel<bool>.Success(result, "成功啦");
                }
                return ResultModel<bool>.Error("更新失败");
            }
            return ResultModel<bool>.Error("模型校验失败");
        }
        [HttpPut]
        public async Task<ResultModel<bool>> EnabledMenuAsync(MenuEnabledPut model)
        {
            if (ModelState.IsValid)
            {
                var result = await _menuService.UpdateAsync(model.Id, model.IsEnabled);
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
