using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DotNET8.WebAPI.JwtHelper
{
    /// <summary>
    /// Jwt服务注册
    /// </summary>
    public static class JwtServiceCollection
    {
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var jwtConfigs = JwtConfiguration.SetJwtConfiguration(configuration);
            services.AddSingleton(jwtConfigs);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = jwtConfigs.TokenValidationParameters;
              options.Events = jwtConfigs.JwtBearerEvents;
          });

            return services;
        }
    }
}