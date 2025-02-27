using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Entities;

namespace Xin.Model
{
    /// <summary>
    /// 角色菜单表
    /// </summary>
    [Table(Name = "sys_role_menu")]
    public class RoleMenuEntity: EntityAdd
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Column(Position = 1,IsPrimary = true)]
        public long RoleId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Column(Position = 2, IsPrimary = true)]
        public long MenuId { get; set; }
    }
}
