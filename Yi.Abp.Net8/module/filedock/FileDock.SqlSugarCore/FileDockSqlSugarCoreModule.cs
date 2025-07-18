using FileDock.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.AuditLogging.SqlSugarCore;
using Yi.Framework.Mapster;
using Yi.Framework.Rbac.SqlSugarCore;
using Yi.Framework.SettingManagement.SqlSugarCore;
using Yi.Framework.SqlSugarCore;
using Yi.Framework.TenantManagement.SqlSugarCore;

namespace FileDock.SqlSugarCore;

[DependsOn(
    typeof(FileDockDomainModule),
    typeof(YiFrameworkRbacSqlSugarCoreModule),
    typeof(YiFrameworkSettingManagementSqlSugarCoreModule),
    typeof(YiFrameworkAuditLoggingSqlSugarCoreModule),
    typeof(YiFrameworkTenantManagementSqlSugarCoreModule),
    typeof(YiFrameworkMapsterModule),
    typeof(YiFrameworkSqlSugarCoreModule)
)]
public class FileDockSqlSugarCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddYiDbContext<FileDockContext>();
        //默认不开放，可根据项目需要是否Db直接对外开放
        //context.Services.AddTransient(x => x.GetRequiredService<ISqlSugarDbContext>().SqlSugarClient);
    }
}