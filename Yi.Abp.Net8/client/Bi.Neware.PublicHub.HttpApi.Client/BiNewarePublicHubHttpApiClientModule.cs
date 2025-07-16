using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Yi.Framework.Rbac.Application.Contracts;

namespace Bi.Neware.PublicHub.HttpApi.Client
{
    [DependsOn(typeof(AbpHttpClientModule),
            typeof(AbpAutofacModule),


        typeof(YiFrameworkRbacApplicationContractsModule))]
    public class BiNewarePublicHubHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //创建动态客户端代理
            context.Services.AddHttpClientProxies(
                typeof(YiFrameworkRbacApplicationContractsModule).Assembly

            );
            Configure<AbpRemoteServiceOptions>(options =>
            {
                options.RemoteServices.Default =
                    new RemoteServiceConfiguration("http://localhost:19001");
            });
        }


    }
}
