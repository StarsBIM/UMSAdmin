using DotNET8.WebAPI.JwtHelper;
using DotNET8.WebAPI.SwaggerHelper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UMS.Core.DB;
using UMS.Core.Extensions;
using UMS.WebAPI.Extensions;

namespace UMS.WebAPI
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // 配置log4net
            services.AddLog4Net();
            // 配置SQL Server
            services.AddDbContext<DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlConn")));
            // 配置允许跨域
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder => builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            // 添加Jwt服务配置
            services.AddJwtService(Configuration);
            // 添加Swagger配置
            services.AddSwagger(Configuration);
            // 添加AutoMapper配置
            services.AddAutoMapper(typeof(AutoMapperProFile));
            //设置Json返回的日期格式
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //启用跨域问题
            app.UseCors(MyAllowSpecificOrigins);

            app.UseForwardedHeaders();

            //在 Routing 之后但在任何 HTTP 输出中间件之前添加，最重要的是在app.UseEndpoints()之前
            app.UseAuthentication();//添加jwt验证

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
