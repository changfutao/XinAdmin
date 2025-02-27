using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.Role.Dto
{
    public class RoleAddInput
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称必填")]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        [Required(ErrorMessage = "角色编码必填")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序必填")]
        public int Sort { get; set; }
    }
}
