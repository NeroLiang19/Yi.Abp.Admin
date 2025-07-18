using Volo.Abp.Collections;

namespace Yi.Framework.SettingManagement.Domain;

public class SettingManagementOptions
{
    public SettingManagementOptions()
    {
        Providers = new TypeList<ISettingManagementProvider>();
    }

    public ITypeList<ISettingManagementProvider> Providers { get; }
}