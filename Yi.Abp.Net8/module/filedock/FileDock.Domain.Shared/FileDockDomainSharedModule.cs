using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Yi.Framework.AuditLogging.Domain.Shared;
using Yi.Framework.Rbac.Domain.Shared;

namespace FileDock.Domain.Shared;

[DependsOn(
    typeof(YiFrameworkRbacDomainSharedModule),
    typeof(YiFrameworkAuditLoggingDomainSharedModule),
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpDddDomainSharedModule))]
public class FileDockDomainSharedModule : AbpModule
{
}