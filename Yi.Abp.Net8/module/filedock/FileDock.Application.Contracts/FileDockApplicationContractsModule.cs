using FileDock.Domain.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Yi.Framework.Ddd.Application.Contracts;
using Yi.Framework.Rbac.Application.Contracts;
using Yi.Framework.TenantManagement.Application.Contracts;

namespace FileDock.Application.Contracts
{
    [DependsOn(
        typeof(FileDockDomainSharedModule),
        typeof(YiFrameworkRbacApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(YiFrameworkTenantManagementApplicationContractsModule),
        typeof(YiFrameworkDddApplicationContractsModule))]
    public class FileDockApplicationContractsModule:AbpModule
    {

    }
}