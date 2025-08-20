using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Yi.Abp.Test;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpUnitOfWorkModule)
)]
public class YiAbpTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}