using FileDock.Domain.Shared;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.AuditLogging.Domain;
using Yi.Framework.Mapster;
using Yi.Framework.Rbac.Domain;
using Yi.Framework.SettingManagement.Domain;
using Yi.Framework.TenantManagement.Domain;

namespace FileDock.Domain;

[DependsOn(
    typeof(FileDockDomainSharedModule),
    typeof(YiFrameworkTenantManagementDomainModule),
    typeof(YiFrameworkRbacDomainModule),
    typeof(YiFrameworkAuditLoggingDomainModule),
    typeof(YiFrameworkSettingManagementDomainModule),
    typeof(YiFrameworkMapsterModule),
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule)
)]
public class FileDockDomainModule : AbpModule
{
}