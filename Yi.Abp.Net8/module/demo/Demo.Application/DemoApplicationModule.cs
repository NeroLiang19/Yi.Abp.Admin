using Demo.Application.Contracts;
using Demo.Domain;
using Volo.Abp.Modularity;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.SettingManagement.Application;
using Yi.Framework.TenantManagement.Application;

namespace Demo.Application;

[DependsOn(
    typeof(DemoApplicationContractsModule),
    typeof(DemoDomainModule),
    typeof(YiFrameworkRbacApplicationModule),
    typeof(YiFrameworkTenantManagementApplicationModule),
    typeof(YiFrameworkSettingManagementApplicationModule),
    typeof(YiFrameworkDddApplicationModule)
)]
public class DemoApplicationModule : AbpModule;