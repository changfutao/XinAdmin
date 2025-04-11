using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Model.Enums;

namespace Xin.Service.User.Dto
{
    public class UserAddInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名必填")]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Phonenumber { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum? Sex { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "状态必填")]
        public UserStatusEnum Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        /// <summary>
        /// 头像Id
        /// </summary>

        public long? AvatorId { get; set; }
    }
}
