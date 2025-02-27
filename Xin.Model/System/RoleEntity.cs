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
    /// 角色表
    /// </summary>
    [Table(Name = "sys_role")]
    public class RoleEntity: EntityFull
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Column(Position = 2, StringLength = 50, IsNullable = false)]
        [Description("角色名称")]
        public string Name { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        [Column(Position = 3, StringLength = 50, IsNullable = false)]
        [Description("角色编码")]
        public string Code { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(Position = 4, StringLength = 200)]
        [Description("备注")]
        public string? Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(Position = 5)]
        [Description("排序")]
        public int Sort { get; set; }
    }
}
