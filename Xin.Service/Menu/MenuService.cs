using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Helpers;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.Menu.Dto;
using Xin.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Xin.Service.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IFreeSql<SqlServerFlag> _fsql;

        public MenuService(IFreeSql<SqlServerFlag> fsql)
        {
            _fsql = fsql;
        }
        /// <summary>
        /// 根据用户Id获取菜单列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MenuDto>> GetMenusByUserIdAsync(long id)
        {
            var menus = await _fsql.Select<MenuEntity, RoleMenuEntity, UserRoleEntity>()
                .InnerJoin((a, b, c) => a.Id == b.MenuId)
                .InnerJoin((a, b, c) => b.RoleId == c.RoleId && c.UserId == id)
                .Where((a, b, c) => (a.MenuType == MenuTypeEnum.Group || a.MenuType == MenuTypeEnum.Menu) && a.IsVisible)
                .ToListAsync<MenuDto>((a, b, c) => new MenuDto
                {
                    Id = a.Id,
                    Icon = a.Icon,
                    FrontName = a.FrontName,
                    FrontPath = a.FrontPath,
                    FrontRoutePath = a.FrontRoutePath,
                    IsFrame = a.IsFrame,
                    MenuName = a.MenuName,
                    MenuType = a.MenuType
                });
            // 顶级菜单
            var topMenus = menus.Where(a => a.ParentId == 0).ToList();
            foreach (var topMenu in topMenus)
            {
                var children = CreateMenus(topMenu.Id, menus);
                if(children != null)
                {
                    topMenu.Children = children;
                }
            }
            return topMenus;
        }
        /// <summary>
        /// 递归获取菜单列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        [NonAction]
        private List<MenuDto>? CreateMenus(long parentId, List<MenuDto> menus)
        {
            var children = menus.Where(a => a.ParentId == parentId).ToList();
            if (children.Any())
            {
                foreach (var item in children)
                {
                    var sunChildren = CreateMenus(item.Id, menus);
                    if (sunChildren != null && sunChildren.Any())
                    {
                        item.Children = sunChildren;
                    }
                    else
                    {
                        item.Children = null;
                    }
                }
                return children;
            }
            else
            {
                return null;
            }
        }
    }
}
