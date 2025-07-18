using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.AuditLogging.Domain;
using Yi.Framework.AuditLogging.Domain.Repositories;
using Yi.Framework.AuditLogging.SqlSugarCore.Repositories;
using Yi.Framework.SqlSugarCore;

namespace Yi.Framework.AuditLogging.SqlSugarCore;

[DependsOn(
    typeof(YiFrameworkAuditLoggingDomainModule),
    typeof(YiFrameworkSqlSugarCoreModule))]
public class YiFrameworkAuditLoggingSqlSugarCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IAuditLogRepository, SqlSugarCoreAuditLogRepository>();
    }
}