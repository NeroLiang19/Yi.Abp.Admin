using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volo.Abp.Testing;

namespace Demo.Test;

/// <summary>
/// Demo模块测试基类
/// </summary>
public class DemoTestBase : AbpTestBaseWithServiceProvider
{
    public DemoTestBase()
    {
        // 创建主机
        var host = Host.CreateDefaultBuilder()
            .UseAutofac()
            .ConfigureServices((host, service) =>
            {
                ConfigureServices(host, service);
                service.AddLogging(builder => builder.ClearProviders().AddConsole().AddDebug());
                service.AddApplicationAsync<DemoTestModule>().Wait();
            })
            .ConfigureAppConfiguration(ConfigureAppConfiguration)
            .Build();

        // 初始化服务提供者
        ServiceProvider = host.Services;
        TestServiceScope = ServiceProvider.CreateScope();
        Logger = (ILogger)ServiceProvider.GetRequiredService(typeof(ILogger<>).MakeGenericType(GetType()));

        // 初始化主机
        host.InitializeAsync().Wait();
    }

    /// <summary>
    /// 日志记录器
    /// </summary>
    public ILogger Logger { get; private set; }
    
    /// <summary>
    /// 测试服务范围
    /// </summary>
    protected IServiceScope TestServiceScope { get; }

    /// <summary>
    /// 配置服务
    /// </summary>
    public virtual void ConfigureServices(HostBuilderContext host, IServiceCollection service)
    {
        // 子类可以重写此方法以添加额外的服务配置
    }

    /// <summary>
    /// 配置应用程序配置
    /// </summary>
    protected virtual void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddJsonFile("appsettings.json");
    }
}