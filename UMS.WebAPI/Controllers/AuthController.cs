using DotNET8.WebAPI.JwtHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UMS.Common;
using UMS.Core.DTO;
using UMS.Core.IService;
using UMS.WebAPI.Models;

namespace DotNET8.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;
        private readonly IAuthService _authService;
        private readonly JwtConfiguration _jwtConfiguration;
        public AuthController(IAdminUserService adminUserService, IAuthService authService, JwtConfiguration jwtConfiguration)
        {
            _adminUserService = adminUserService;
            _authService = authService;
            _jwtConfiguration = jwtConfiguration;
        }
        /// <summary>
        /// 使用用户名登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]//设置登录接口允许匿名
        public async Task<ResultModel<AdminUserLoginDTO>> LoginAsync(AdminUserLoginPost model)
        {
            if (ModelState.IsValid)
            {
                var adminUser = await _authService.LoginAsync(model.Name, model.Password);
                if (adminUser == null)
                {
                    return ResultModel<AdminUserLoginDTO>.Error("登陆失败，账号或密码错误!", 401);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, adminUser.Name)
                };

                foreach (var role in adminUser.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                var key = new SymmetricSecurityKey(_jwtConfiguration.Key);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken
                (
                    issuer: _jwtConfiguration.Issuer, // 颁发者
                    audience: _jwtConfiguration.Audience, // 接收者
                    expires: DateTime.Now.AddMinutes(120),//过期时间（可自行设定，注意和上面的claims内部Exp参数保持一致）
                    signingCredentials: creds, // 签名证书
                    claims: claims.ToArray()// 自定义参数
                );
                AdminUserLoginDTO adminUserLogin = new AdminUserLoginDTO()
                {
                    AdminUser = adminUser,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                };
                return ResultModel<AdminUserLoginDTO>.Success(adminUserLogin, "登陆成功!");
            }
            else
            {
                return ResultModel<AdminUserLoginDTO>.Error("登陆失败，账号或密码错误!", 401);
            }
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="AccessToken">旧Token</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResultModel<object> RefreshToken(string AccessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                if (AccessToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    AccessToken = AccessToken["Bearer ".Length..].Trim();
                }

                // 根据过期token获取一个 SecurityToken 
                var principal = tokenHandler.ValidateToken(AccessToken, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_jwtConfiguration.Key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false, // 不对token过期进行验证
                }, out SecurityToken validatedToken);

                // 算法验证
                if (validatedToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return ResultModel<object>.Error("Token令牌无效", StatusCodes.Status400BadRequest);
                }

                var expires = DateTime.Now.AddDays(1);//有效期设置为1天
                var key = new SymmetricSecurityKey(_jwtConfiguration.Key);
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken
                (
                    issuer: _jwtConfiguration.Issuer,//发布者
                    audience: _jwtConfiguration.Audience,//接收者
                    claims: principal.Claims,//存放的用户信息
                    notBefore: DateTime.UtcNow,//发布时间
                    expires: expires,//有效期
                    signingCredentials: creds//数字签名
                );

                var data = new
                {
                    Expires = expires.ToString("yyyy-MM-dd HH:mm:ss"),
                    AccessToken,
                    RefreshToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token)
                };
                return ResultModel<object>.Success(data, "刷新成功");
            }
            catch (Exception ex)
            {
                return ResultModel<object>.Error($"系统错误 + {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ResultModel<string> NoPermission()
        {
            return ResultModel<string>.Success("No Permission!");
        }
    }
}
