using Xin.Model;

namespace Xin.Service.Menu.Dto;

public class MenuPermsDto
{
    /// <summary>
    /// 菜单列表
    /// </summary>
    public List<MenuDto> Menus { get; set; }
    /// <summary>
    /// 按钮权限
    /// </summary>
    public List<string> Buttons { get; set; }
}