using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.User.Dto
{
    /// <summary>
    /// 用户Dto
    /// </summary>
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
