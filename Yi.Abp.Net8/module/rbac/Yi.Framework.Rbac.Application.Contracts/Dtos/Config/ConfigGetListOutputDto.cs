using Volo.Abp.Application.Dtos;

namespace Yi.Framework.Rbac.Application.Contracts.Dtos.Config;

public class ConfigGetListOutputDto : EntityDto<Guid>
{
    public Guid Id { get; set; }

    /// <summary>
    ///     ��������
    /// </summary>
    public string ConfigName { get; set; } = string.Empty;

    /// <summary>
    ///     ��������
    /// </summary>
    public string ConfigKey { get; set; } = string.Empty;

    /// <summary>
    ///     ����ֵ
    /// </summary>
    public string ConfigValue { get; set; } = string.Empty;

    /// <summary>
    ///     ��������
    /// </summary>
    public string? ConfigType { get; set; }

    /// <summary>
    ///     �����ֶ�
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    ///     ��ע
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    ///     ����ʱ��
    /// </summary>
    public DateTime CreationTime { get; set; }
}