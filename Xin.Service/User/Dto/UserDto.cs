using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Model.Enums;

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
        public string? Phonenumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum? Sex { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public UserStatusEnum Status { get; set; }
        /// <summary>
        /// 用户头像Id
        /// </summary>

        public long? AvatorId { get; set; }
        /// <summary>
        /// 用户头像路径
        /// </summary>

        public string? AvatorPath { get; set; }
    }
}
