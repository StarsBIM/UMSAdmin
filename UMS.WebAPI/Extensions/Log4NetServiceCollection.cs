namespace UMS.WebAPI.Extensions
{
    /// <summary>
    /// log4net服务注册
    /// </summary>
    public static class Log4NetServiceCollection
    {
        public static IServiceCollection AddLog4Net(this IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options.AddLog4Net(new Log4NetProviderOptions() { Log4NetConfigFileName = "log4net.config", Watch = true });
            });
            return services;
        }
    }
}
