using FileDock.Application.Contracts;
using FileDock.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.SettingManagement.Application;
using Yi.Framework.TenantManagement.Application;

namespace FileDock.Application
{
    [DependsOn(
        typeof(FileDockApplicationContractsModule),
        typeof(FileDockDomainModule),
        typeof(YiFrameworkRbacApplicationModule),
        typeof(YiFrameworkTenantManagementApplicationModule),
        typeof (YiFrameworkSettingManagementApplicationModule),
        typeof(YiFrameworkDddApplicationModule)
        )]
    public class FileDockApplicationModule : AbpModule
    {
    }
}
