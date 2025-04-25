using System.Reflection.Metadata.Ecma335;

namespace Xin.Service.Menu.Dto;

public class MenuItemDto
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; }
    /// <summary>
    /// 前端地址
    /// </summary>
    public string? FrontPath { get; set; }
    /// <summary>
    /// 前端路由
    /// </summary>
    public string? Component { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }
    /// <summary>
    /// 子级菜单
    /// </summary>
    public List<MenuItemDto>? Menus { get; set; }
    /// <summary>
    /// 权限点
    /// </summary>
    public List<string>? Buttons   { get; set; }
}