using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace Yi.Framework.SettingManagement.Domain;

public class UserSettingManagementProvider : SettingManagementProvider, ITransientDependency
{
    public UserSettingManagementProvider(
        ISettingManagementStore settingManagementStore,
        ICurrentUser currentUser)
        : base(settingManagementStore)
    {
        CurrentUser = currentUser;
    }

    public override string Name => UserSettingValueProvider.ProviderName;

    protected ICurrentUser CurrentUser { get; }

    protected override string NormalizeProviderKey(string providerKey)
    {
        if (providerKey != null) return providerKey;

        return CurrentUser.Id?.ToString();
    }
}