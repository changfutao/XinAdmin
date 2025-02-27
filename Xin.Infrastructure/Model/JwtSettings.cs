using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure
{
    /// <summary>
    /// JwtSetteings实体类
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Token签发者(发布者)
        /// </summary>
        public string Issuer { get; set; } = "http://localhost:8888/";

        /// <summary>
        /// Token订阅者
        /// </summary>
        public string Audience { get; set; } = "http://localhost:8887/";
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; } = "asdasd123123wqasdasd123@13123asdasd123123";
        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int Expire { get; set; } = 1440;
        /// <summary>
        /// Token刷新时间
        /// </summary>
        public int RefreshTokenTime { get; set; } = 30;
        /// <summary>
        /// Token类型
        /// </summary>
        public string TokenType { get; set; } = "Bearer";
    }
}
