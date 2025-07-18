using Volo.Abp.DependencyInjection;
using Yi.Framework.SqlSugarCore;

namespace FileDock.SqlSugarCore
{
    public class FileDockContext : SqlSugarDbContext
    {
        public FileDockContext(IAbpLazyServiceProvider lazyServiceProvider) : base(lazyServiceProvider)
        {
        }
    }
}
