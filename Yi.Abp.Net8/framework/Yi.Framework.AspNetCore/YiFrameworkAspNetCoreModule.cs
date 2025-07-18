using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.AspNetCore.WebClientInfo;
using Yi.Framework.Core;

namespace Yi.Framework.AspNetCore;

/// <summary>
///     Yi框架ASP.NET Core模块
/// </summary>
[DependsOn(typeof(YiFrameworkCoreModule))]
public class YiFrameworkAspNetCoreModule : AbpModule
{
    /// <summary>
    ///     配置服务后的处理
    /// </summary>
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;

        // 替换默认的WebClientInfoProvider为支持代理的实现
        services.Replace(new ServiceDescriptor(
            typeof(IWebClientInfoProvider),
            typeof(RealIpHttpContextWebClientInfoProvider),
            ServiceLifetime.Transient));
    }
}