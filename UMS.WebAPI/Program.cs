using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using UMS.Core.IService;
using UMS.WebAPI;

partial class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
    {
        //获取所有相关类库的程序集,并将实现IServiceSupport接口的程序集注入Autofac容器
        builder.RegisterAssemblyTypes(Assembly.Load("UMS.Application"))
        .Where(type => !type.IsAbstract && typeof(IServiceSupport)
        .IsAssignableFrom(type))
        .AsImplementedInterfaces();
    }).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}

