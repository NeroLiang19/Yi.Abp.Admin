using Volo.Abp.DependencyInjection;
using Yi.Framework.SqlSugarCore;

namespace Demo.SqlSugarCore;

public class DemoContext : SqlSugarDbContext
{
    public DemoContext(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
    {
    }
}