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
    /// 用户角色表
    /// </summary>
    [Table(Name = "sys_user_role")]
    public class UserRoleEntity: EntityAdd
    {
        [Column(Position = 2)]
        public long UserId { get; set; }
        [Column(Position = 3)]
        public long RoleId { get; set; }
    }
}
