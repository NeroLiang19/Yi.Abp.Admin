using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Yi.Abp.Test;

public class YiAbpTestBase : AbpTestBaseWithServiceProvider
{
    public YiAbpTestBase()
    {
        var host = Host.CreateDefaultBuilder()
            .UseAutofac()
            .ConfigureServices((host, service) =>
            {
                ConfigureServices(host, service);
                service.AddLogging(builder => builder.ClearProviders().AddConsole().AddDebug());
                /*application= */
                service.AddApplicationAsync<YiAbpTestModule>().Wait();
            })
            .ConfigureAppConfiguration(ConfigureAppConfiguration)
            .Build();

        ServiceProvider = host.Services;
        TestServiceScope = ServiceProvider.CreateScope();
        Logger = (ILogger)ServiceProvider.GetRequiredService(typeof(ILogger<>).MakeGenericType(GetType()));

        host.InitializeAsync().Wait();
    }

    public ILogger Logger { get; private set; }
    protected IServiceScope TestServiceScope { get; }


    public virtual void ConfigureServices(HostBuilderContext host, IServiceCollection service)
    {
    }

    protected virtual void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddJsonFile("appsettings.json");
        configurationBuilder.AddJsonFile("appsettings.Development.json");
    }
}