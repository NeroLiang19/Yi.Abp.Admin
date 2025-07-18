using Volo.Abp.Application;

namespace Yi.Framework.Ddd.Application.Contracts;

/// <summary>
///     Yi框架DDD应用层契约模块
/// </summary>
[DependsOn(typeof(AbpDddApplicationContractsModule))]
public class YiFrameworkDddApplicationContractsModule : AbpModule
{
}