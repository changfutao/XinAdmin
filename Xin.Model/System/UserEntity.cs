using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Entities;
using Xin.Model.Enums;

namespace Xin.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table(Name = "sys_user")]
    public class UserEntity : EntityFull, ITenant<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(Position = 2, StringLength = 20, IsNullable = false)]
        [Description("用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Column(Position = 3, StringLength = 20)]
        [Description("昵称")]
        public string? NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column(Position = 4, StringLength = 20, IsNullable = false)]
        [Description("密码")]
        public string Password { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Column(Position = 5,StringLength = 20)]
        [Description("手机号")]
        public string? Phonenumber { get; set; }
        /// <summary>
        /// 用户性别（0男 1女 2未知）
        /// </summary>
        [Column(Position = 6)]
        [Description("用户性别")]
        public SexEnum? Sex { get; set; }
        /// <summary>
        /// 状态 
        /// </summary>
        [Column(Position =7)]
        [Description("状态 0 在职 1 锁定 2 离职")]
        public UserStatusEnum Status { get; set; }
        /// <summary>
        /// 密码错误次数
        /// </summary>
        [Column(Position = 8)]
        [Description("密码错误次数, 3次以后当天不能登录")]
        public int ErrorCount { get; set; }
        /// <summary>
        /// 锁定日期
        /// </summary>
        [Column(Position = 9)]
        [Description("锁定日期")]
        public DateTime? LockDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column(Position = 10,StringLength = 500)]
        [Description("备注")]
        public string? Remark { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [Column(Position = 11, StringLength = 20)]
        [Description("最后登录IP")]
        public string? LoginIP { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Column(Position = 12)]
        [Description("最后登录时间")]
        public DateTime? LoginDate { get; set; }
        /// <summary>
        /// 头像Id
        /// </summary>
        [Column(Position = 13)]
        [Description("头像Id")]
        public long? AvatorId { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        [Column(Position = -9)]
        [Description("租户Id")]
        public long? TenantId { get; set; }
    }
}
