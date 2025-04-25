using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Service.Menu.Dto;

namespace Xin.Service.Menu
{
    public interface IMenuService
    {
        /// <summary>
        /// 根据用户Id获取菜单列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuPermsDto> GetMenusByUserIdAsync(long id);
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> GetMenuTreeData(MenuInput input);
        /// <summary>
        /// 获取菜单树(除了按钮)
        /// </summary>
        /// <returns></returns>
        Task<IResultOutput> GetMenuTreeDataExceptButton();
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> AddMenu(MenuAddInput input);
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> EditMenu(MenuEditInput input);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultOutput> DeleteMenu(long id);
        /// <summary>
        /// 根据角色Id获取菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IResultOutput> GetMenusByRoleId(long roleId);
    }
}
