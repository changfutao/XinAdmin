using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Entities;

namespace Xin.Model
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table(Name = "sys_menu")]
    public class MenuEntity: EntityFull
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Column(Position = 2, StringLength = 30, IsNullable = false)]
        [Description("菜单名称")]
        public string MenuName { get; set; }
        /// <summary>
        /// 父菜单Id
        /// </summary>
        [Column(Position = 3)]
        [Description("父菜单Id")]
        public long ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(Position = 4)]
        [Description("排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 后台路由地址
        /// </summary>
        [Column(Position = 5, StringLength = 50)]
        [Description("后台路由地址")]
        public string? Path { get; set; }
        /// <summary>
        /// 前台路由name
        /// </summary>
        [Column(Position = 6, StringLength = 50)]
        [Description("前台路由name")]
        public string? FrontName { get; set; }
        /// <summary>
        /// 前台文件地址
        /// </summary>
        [Column(Position = 7, StringLength = 50)]
        [Description("前台文件地址")]
        public string? FrontPath { get; set; }
        /// <summary>
        /// 前端路由地址
        /// </summary>
        [Column(Position = 8, StringLength = 50)]
        [Description("前端路由地址")]
        public string? FrontRoutePath { get; set; }
        /// <summary>
        /// 是否外链
        /// </summary>
        [Column(Position = 9)]
        [Description("是否外链")]
        public bool IsFrame { get; set; }
        /// <summary>
        /// 菜单类型(0 目录 1 菜单 2 按钮 3 链接)
        /// </summary>
        [Column(Position = 10)]
        [Description("菜单类型(0 目录 1 菜单 2 权限点 3 链接)")]
        public MenuTypeEnum MenuType { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [Column(Position = 11)]
        [Description("是否显示")]
        public bool IsVisible { get; set; }
        /// <summary>
        /// 权限字符串
        /// </summary>
        [Column(Position = 12, StringLength = 50)]
        [Description("权限字符串")]
        public string? Perms { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Column(Position = 13, StringLength = 30)]
        [Description("图标")]
        public string? Icon { get; set; }
    }

    public enum MenuTypeEnum
    {
        /// <summary>
        /// 分组
        /// </summary>
        Group,
        /// <summary>
        /// 菜单
        /// </summary>
        Menu,
        /// <summary>
        /// 权限点
        /// </summary>
        Dot
    }
}
