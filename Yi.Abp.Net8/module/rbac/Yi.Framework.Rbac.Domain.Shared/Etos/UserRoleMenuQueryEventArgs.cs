using Yi.Framework.Rbac.Domain.Shared.Dtos;

namespace Yi.Framework.Rbac.Domain.Shared.Etos;

public class UserRoleMenuQueryEventArgs
{
    public UserRoleMenuQueryEventArgs()
    {
    }

    public UserRoleMenuQueryEventArgs(params Guid[] userIds)
    {
        UserIds.AddRange(userIds.ToList());
    }

    public List<Guid> UserIds { get; set; } = new();

    public List<UserRoleMenuDto>? Result { get; set; }
}