using System.ComponentModel;

namespace Xin.Model.Enums;

/// <summary>
/// 用户状态
/// </summary>
public enum UserStatusEnum
{
    [Description("在职")]
    Normal,
    [Description("锁定")]
    Locked,
    [Description("离职")]
    Resign
}