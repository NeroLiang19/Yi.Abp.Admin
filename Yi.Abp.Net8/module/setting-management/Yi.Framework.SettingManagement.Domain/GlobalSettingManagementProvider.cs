using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Yi.Framework.SettingManagement.Domain;

public class GlobalSettingManagementProvider : SettingManagementProvider, ITransientDependency
{
    public GlobalSettingManagementProvider(ISettingManagementStore settingManagementStore)
        : base(settingManagementStore)
    {
    }

    public override string Name => GlobalSettingValueProvider.ProviderName;

    protected override string NormalizeProviderKey(string providerKey)
    {
        return null;
    }
}