using Volo.Abp.Autofac;

namespace Yi.Abp.Test
{
    [DependsOn(
        typeof(AbpAutofacModule)
        )]
    public class YiAbpTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
