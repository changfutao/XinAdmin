using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.User.Dto
{
    public class ChangePwdDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage ="用户Id不能为空")]
        public long Id { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage ="请输入旧密码")]
        public string OriginPwd { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "请输入新密码")]
        public string NewPwd { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "请输入确认密码")]
        public string ConfirmPwd { get; set; }
    }
}
