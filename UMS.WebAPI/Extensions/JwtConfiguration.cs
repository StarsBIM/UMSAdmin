using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNET8.WebAPI.JwtHelper
{
    /// <summary>
    /// Jwt配置
    /// </summary>
    public class JwtConfiguration
    {
        public JwtConfiguration(byte[] key, string issuer, string audience)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
        }

        /// <summary>
        /// Key
        /// </summary>
        public byte[] Key { get; set; }

        /// <summary>
        ///令牌的颁发者
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// 颁发给谁
        /// </summary>
        public string Audience { get; }

        /// <summary>
        /// 令牌验证参数
        /// </summary>
        public TokenValidationParameters TokenValidationParameters => new()
        {
            //验证Issuer和Audience
            ValidateIssuer = true,//验证发行人
            ValidateAudience = true,//验证订阅人
            ValidateIssuerSigningKey = true,//验证秘钥
            //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
            ValidateLifetime = true,//验证失效时间
            RequireExpirationTime = true,//要求token中包含过期时间
            //注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟
            ClockSkew = TimeSpan.FromSeconds(4),//允许过期时间的误差范围
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Key),
            //这里采用动态验证的方式，在重新登陆时，刷新token，旧token就强制失效了
            AudienceValidator = (m, n, z) =>
            {
                return m != null && m.FirstOrDefault().Equals(Audience);
            }

        };

        /// <summary>
        /// 令牌验证事件
        /// </summary>
        public JwtBearerEvents JwtBearerEvents => new()
        {
            //权限验证失败后执行
            OnChallenge = context =>
            {
                //终止默认的返回结果
                context.HandleResponse();
                string token = context.Request.Headers["Authorization"];
                var result = JsonConvert.SerializeObject(new { Code = StatusCodes.Status401Unauthorized, Message = "token已过期" }); ;
                if (string.IsNullOrEmpty(token))
                {
                    //验证失败返回401
                    result = JsonConvert.SerializeObject(new { Code = StatusCodes.Status401Unauthorized, Message = "token不能为空" });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.WriteAsync(result);
                    return Task.FromResult(result);
                }

                try
                {
                    JwtSecurityTokenHandler tokenheader = new();
                    ClaimsPrincipal claimsPrincipal = tokenheader.ValidateToken(token, TokenValidationParameters, out SecurityToken securityToken);
                }
                catch (SecurityTokenExpiredException)
                {
                    //验证失败返回401
                    result = JsonConvert.SerializeObject(new { Code = StatusCodes.Status401Unauthorized, Message = "token已过期" });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.WriteAsync(result);
                    return Task.FromResult(result);
                }
                catch (Exception)
                {
                    //验证失败返回401
                    result = JsonConvert.SerializeObject(new { Code = StatusCodes.Status401Unauthorized, Message = "token无效" });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.WriteAsync(result);
                    return Task.FromResult(result);
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.WriteAsync(result);
                return Task.FromResult(result);
            }
        };

        /// <summary>
        /// 设置Jwt配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static JwtConfiguration SetJwtConfiguration(IConfiguration configuration)
        {
            var issuser = configuration["Authentication:JwtBearer:Issuer"] ?? "default_issuer";
            var auidence = configuration["Authentication:JwtBearer:Audience"] ?? "default_auidence";
            var securityKey = configuration["Authentication:JwtBearer:SecurityKey"] ?? "default_securitykey";

            byte[] key = Encoding.ASCII.GetBytes(securityKey);

            return new JwtConfiguration(key, issuser, auidence);
        }
    }
}