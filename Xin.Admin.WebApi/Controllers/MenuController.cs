
using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Dto;
using Xin.Service.Menu;
using Xin.Service.Menu.Dto;

namespace Xin.Admin.WebApi.Controllers;
/// <summary>
/// 菜单管理
/// </summary>
[ApiExplorerSettings(GroupName = "菜单管理")]
public class MenuController : BaseController
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }
    /// <summary>
    /// 菜单树
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultOutput> GetMenuTreeData(MenuInput input)
    {
        return _menuService.GetMenuTreeData(input);
    }
    /// <summary>
    /// 菜单树(除了按钮)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultOutput> GetMenuTreeDataExceptButton()
    {
        return _menuService.GetMenuTreeDataExceptButton();
    }
    /// <summary>
    /// 新增菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultOutput> AddMenu(MenuAddInput input)
    {
        return _menuService.AddMenu(input);
    }
    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultOutput> EditMenu(MenuEditInput input)
    {
        return _menuService.EditMenu(input);
    }
    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultOutput> DeleteMenu(long id)
    {
        return _menuService.DeleteMenu(id);
    }
    /// <summary>
    /// 根据角色Id获取菜单列表
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultOutput> GetMenusByRoleId(long roleId)
    {
        return _menuService.GetMenusByRoleId(roleId);
    }
}