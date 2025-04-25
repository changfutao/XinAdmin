namespace Xin.Service.Menu.Dto;

public class MenuInput
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string? MenuName { get; set; }
    /// <summary>
    /// 是否可见
    /// </summary>
    public int? IsVisible { get; set; }
}