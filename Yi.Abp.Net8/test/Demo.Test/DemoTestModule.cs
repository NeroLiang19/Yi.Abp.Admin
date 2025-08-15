using Demo.Application;
using Demo.Domain.Entities;
using Demo.SqlSugarCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Demo.Test;

/// <summary>
/// Demo测试模块
/// </summary>
[DependsOn(
    typeof(DemoApplicationModule),
    typeof(DemoSqlSugarCoreModule),
    typeof(AbpAutofacModule)
)]
public class DemoTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // 禁用后台工作者
        Configure<AbpBackgroundWorkerOptions>(options =>
        {
            options.IsEnabled = false;
        });
        
        // 配置数据库连接
        Configure<DbConnOptions>(options =>
        {
            options.Url = $"DataSource=demo-test-{DateTime.Now:yyyyMMdd_HHmmss}.db";
        });
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var services = context.ServiceProvider;
        
        // 这里可以添加测试数据初始化逻辑
        // 例如：创建默认的图书和图书馆
    }
}