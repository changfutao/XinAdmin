using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Mapster;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Helpers;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.Menu.Dto;
using Xin.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Xin.Service.Role.Dto;

namespace Xin.Service.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IFreeSql<SqlServerFlag> _fsql;
        private int _menuNum = 0;
        public MenuService(IFreeSql<SqlServerFlag> fsql)
        {
            _fsql = fsql;
        }

        /// <summary>
        /// 根据用户Id获取菜单列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuPermsDto> GetMenusByUserIdAsync(long id)
        {
            MenuPermsDto dto = new MenuPermsDto();
            var menus = await _fsql.Select<MenuEntity, RoleMenuEntity, UserRoleEntity>()
                .InnerJoin((a, b, c) => a.Id == b.MenuId)
                .InnerJoin((a, b, c) => b.RoleId == c.RoleId && c.UserId == id)
                .Where((a, b, c) => a.IsVisible)
                .ToListAsync<MenuDto>((a, b, c) => new MenuDto
                {
                    Id = a.Id,
                    ParentId = a.ParentId,
                    Icon = a.Icon,
                    FrontName = a.FrontName,
                    FrontPath = a.FrontPath,
                    Component = a.Component,
                    MenuName = a.MenuName,
                    MenuType = a.MenuType,
                    Perms = a.Perms
                });
            var menuDtos = new List<MenuDto>();
            var menuAll = await _fsql.Select<MenuEntity>().ToListAsync<MenuDto>();
            foreach (var menu in menus)
            {
                menuDtos.Add(menu);
                GetMenus(menuAll, menu, menuDtos);
            }
            // 按钮权限列表
            var perms = menus.Where(a => a.MenuType == MenuTypeEnum.Dot && !string.IsNullOrEmpty(a.Perms)).Select(a => a.Perms).ToList();
            dto.Menus = GenerateMenuTree(menuDtos, null, true);
            dto.Buttons = perms;
            return dto;
        }

        /// <summary>
        /// 递归获取菜单列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 根据角色Id获取菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetMenusByRoleId(long roleId)
        {
            RoleMenuTreeDto dto = new RoleMenuTreeDto();
          var menuDtos = await _fsql.Select<RoleMenuEntity, MenuEntity>()
                       .InnerJoin((a, b) => a.MenuId == b.Id)
                       .Where((a, b) => a.RoleId == roleId)
                       .ToListAsync<MenuDto>((a, b) => new MenuDto
                       {
                           Id = b.Id,
                           ParentId = b.ParentId,
                           Icon = b.Icon,
                           FrontName = b.FrontName,
                           FrontPath = b.FrontPath,
                           Component = b.Component,
                           MenuName = b.MenuName,
                           MenuType = b.MenuType,
                           Perms = b.Perms
                       });
          var menus = await _fsql.Select<MenuEntity>().ToListAsync<MenuDto>();
          // 生成菜单树
          var menuList = GenerateMenuTree(menus, null);
          dto.Menus = menuList;
          List<long> menuIds = new List<long>();
          foreach (var item in menuDtos)
          {
              if (!menus.Any(a => a.ParentId == item.Id))
              {
                  menuIds.Add(item.Id);
              }
          }
          dto.MenuIds = menuIds;
          return ResultOutput.Ok(dto);
        }

        #region 获取菜单树
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetMenuTreeData(MenuInput input)
        {
            var menus = await _fsql.Select<MenuEntity>()
                .ToListAsync<MenuDto>();
            List<MenuDto> menuList = menus;
            if (!string.IsNullOrEmpty(input.MenuName))
            {
                menuList = menuList.Where(a => a.MenuName.Contains(input.MenuName)).ToList();
            }

            if (input.IsVisible.HasValue)
            {
                menuList = menuList.Where(a => a.IsVisible == (input.IsVisible.Value == 1)).ToList();
            }
            var menuDtos = new List<MenuDto>();
            foreach (var menu in menuList)
            {
                menuDtos.Add(menu);
                GetMenus(menus, menu, menuDtos);
            }
            // 生成菜单树
            var list = GenerateMenuTree(menuDtos, null);
            return ResultOutput.Ok(list);
        }
        
        /// <summary>
        /// 所有过滤后的部门数据
        /// </summary>
        /// <param name="all"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        private void GetMenus(List<MenuDto> all, MenuDto menu, List<MenuDto> menuList)
        {
            // 获取当前元素的父级
            var parent = all.FirstOrDefault(a => a.Id == menu.ParentId);
            if (parent != null)
            {
                if (!menuList.Any(a => a.Id == parent.Id))
                {
                    menuList.Add(parent);
                }
                GetMenus(all, parent, menuList);
            }
        }
        /// <summary>
        /// 获取除了权限点的菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<IResultOutput> GetMenuTreeDataExceptButton()
        {
            var menus = await _fsql.Select<MenuEntity>()
                .ToListAsync<MenuDto>();
            // 生成菜单树
            var list = GenerateMenuTree(menus, null, true);
            return ResultOutput.Ok(list);
        }

        private List<MenuDto> GenerateMenuTree(List<MenuDto> all, long? pid, bool isFilterButton = false)
        {
            List<MenuDto> list = new List<MenuDto>();
            var children = all.Where(a => a.ParentId == pid);
            if (children.Any())
            {
                foreach (var item in children)
                {
                    if(isFilterButton && item.MenuType == MenuTypeEnum.Dot) continue;
                    list.Add(item);
                    
                    var menus = GenerateMenuTree(all, item.Id, isFilterButton);
                    if (menus.Any())
                        item.Children = menus;
                }
            }
            return list;
        }

        #endregion
        
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> AddMenu(MenuAddInput input)
        {
            // 判断菜单名称是否重复
            if (await _fsql.Select<MenuEntity>().AnyAsync(a => a.MenuName == input.MenuName))
            {
                return ResultOutput.NotOk("菜单名称已存在");
            }
            var menu = input.Adapt<MenuEntity>();
            await _fsql.Insert(menu).ExecuteAffrowsAsync();;
            return ResultOutput.Ok();
        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> EditMenu(MenuEditInput input)
        {
            // 判断菜单名称是否重复
            if (await _fsql.Select<MenuEntity>().AnyAsync(a => a.Id != input.Id && a.MenuName == input.MenuName))
            {
                return ResultOutput.NotOk("菜单名称已存在");
            }
            var menu = input.Adapt<MenuEntity>();
            await _fsql.Update<MenuEntity>()
                .SetSource(menu)
                .ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultOutput> DeleteMenu(long id)
        {
            await _fsql.Update<MenuEntity>()
                .Set(a => a.IsDeleted, true)
                .Where(a => a.Id == id)
                .ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }
    }
}