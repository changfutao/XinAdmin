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
        public async Task<(List<MenuDto>, List<MenuDto>)> GetMenusByUserIdAsync(long id)
        {
            var menus = await _fsql.Select<MenuEntity, RoleMenuEntity, UserRoleEntity>()
                .InnerJoin((a, b, c) => a.Id == b.MenuId)
                .InnerJoin((a, b, c) => b.RoleId == c.RoleId && c.UserId == id)
                .ToListAsync<MenuDto>((a, b, c) => new MenuDto
                {
                    Id = a.Id,
                    Icon = a.Icon,
                    FrontName = a.FrontName,
                    FrontPath = a.FrontPath,
                    FrontRoutePath = a.FrontRoutePath,
                    IsFrame = a.IsFrame,
                    IsVisible = a.IsVisible,
                    MenuName = a.MenuName,
                    MenuType = a.MenuType,
                    Perms = a.Perms
                });
            var topMenus = menus.Where(a => a.ParentId == 0).ToList();
            List<MenuDto> menuList = new List<MenuDto>();
            foreach (var topMenu in topMenus)
            {
                menuList.Add(TransExp<MenuDto, MenuDto>.Trans(topMenu));
                var children = CreateMenus(topMenu.Id, menus);
                if (children != null && children.Any())
                {
                    topMenu.Children = children;
                }
                else
                {
                    topMenu.Children = null;
                }
            }

            var menuStr = JsonSerializer.Serialize(menus, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            });
            var menuNewList = JsonSerializer.Deserialize<List<MenuDto>>(menuStr);
            foreach (var menu in menuList)
            {
                var children = CreateMenus(menu.Id, menuNewList, isFilterButton: true);
                if (children != null && children.Any())
                {
                    menu.Children = children;
                }
                else
                {
                    menu.Children = null;
                }
            }
       
            return (topMenus, menuList);
        }
        /// <summary>
        /// 递归获取菜单列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        private List<MenuDto>? CreateMenus(long parentId, List<MenuDto> menus, bool isFilterButton = false)
        {
            var children = menus.Where(a => a.ParentId == parentId).Where(isFilterButton, a => a.MenuType != 2).ToList();
            if (children.Any())
            {
                foreach (var item in children)
                {
                    var sunChildren = CreateMenus(item.Id, menus, isFilterButton);
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
