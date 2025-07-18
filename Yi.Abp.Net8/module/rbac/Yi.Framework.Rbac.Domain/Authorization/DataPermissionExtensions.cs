using Volo.Abp.Data;

namespace Yi.Framework.Rbac.Domain.Authorization;

public static class DataPermissionExtensions
{
    /// <summary>
    ///     关闭数据权限
    /// </summary>
    /// <param name="dataFilter"></param>
    /// <returns></returns>
    public static IDisposable DisablePermissionHandler(this IDataFilter dataFilter)
    {
        return dataFilter.Disable<IDataPermission>();
    }
}