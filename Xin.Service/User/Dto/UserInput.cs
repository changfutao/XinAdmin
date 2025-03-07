using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Model.Enums;

namespace Xin.Service.User.Dto
{
    /// <summary>
    /// 用户分页查询
    /// </summary>
    public class UserInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public UserStatusEnum? Status { get; set; }
    }
}
