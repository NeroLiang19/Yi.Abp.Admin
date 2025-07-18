using Yi.Framework.Rbac.Domain.Shared.Enums;

namespace Yi.Framework.Rbac.Domain.Shared.Model;

public class RoleTokenInfoModel
{
    public Guid Id { get; set; }
    public DataScopeEnum DataScope { get; set; }
}