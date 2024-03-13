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
        //��ȡ����������ĳ���,����ʵ��IServiceSupport�ӿڵĳ���ע��Autofac����
        builder.RegisterAssemblyTypes(Assembly.Load("UMS.Application"))
        .Where(type => !type.IsAbstract && typeof(IServiceSupport)
        .IsAssignableFrom(type))
        .AsImplementedInterfaces();
    }).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}

