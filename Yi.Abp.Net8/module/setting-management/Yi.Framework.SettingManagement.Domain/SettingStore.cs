using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Yi.Framework.SettingManagement.Domain;

public class SettingStore : ISettingStore, ITransientDependency
{
    public SettingStore(ISettingManagementStore managementStore)
    {
        ManagementStore = managementStore;
    }

    protected ISettingManagementStore ManagementStore { get; }

    public virtual Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
    {
        return ManagementStore.GetOrNullAsync(name, providerName, providerKey);
    }

    public virtual Task<List<SettingValue>> GetAllAsync(string[] names, string providerName, string providerKey)
    {
        return ManagementStore.GetListAsync(names, providerName, providerKey);
    }
}