using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Model;

namespace Xin.Service.Menu.Dto
{
    public class MenuDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 前台路由名称
        /// </summary>
        public string? FrontName { get; set; }
        /// <summary>
        /// 前台文件地址
        /// </summary>
        public string? FrontPath { get; set; }
        /// <summary>
        /// 前台路由地址
        /// </summary>
        public string? FrontRoutePath { get; set; }
        /// <summary>
        /// 是否外链
        /// </summary>
        public bool IsFrame { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// 菜单类型 0 目录 1 菜单 2 按钮
        /// </summary>
        public MenuTypeEnum MenuType { get; set; }
        /// <summary>
        /// 前端按钮权限
        /// </summary>
        public string? Perms { get; set; }
        public List<MenuDto>? Children { get; set; }
    }
}
