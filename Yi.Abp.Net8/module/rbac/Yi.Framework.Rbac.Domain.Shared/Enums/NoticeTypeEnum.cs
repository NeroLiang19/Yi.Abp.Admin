using System.ComponentModel;

namespace Yi.Framework.Rbac.Domain.Shared.Enums;

public enum NoticeTypeEnum
{
    [Description("走马灯")] MerryGoRound = 0,
    [Description("提示弹窗")] Popup = 1
}