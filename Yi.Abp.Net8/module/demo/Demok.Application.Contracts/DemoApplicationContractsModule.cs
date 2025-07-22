using Demo.Domain.Shared;
using Volo.Abp.SettingManagement;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.Rbac.Application.Contracts;
using Yi.Framework.TenantManagement.Application.Contracts;

namespace Demo.Application.Contracts;

[DependsOn(
    typeof(FileDockDomainSharedModule),
    typeof(YiFrameworkRbacApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(YiFrameworkTenantManagementApplicationContractsModule),
    typeof(YiFrameworkDddApplicationContractsModule))]
public class DemoApplicationContractsModule : AbpModule
{
}