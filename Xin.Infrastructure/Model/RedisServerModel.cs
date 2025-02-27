using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure
{
    /// <summary>
    /// Redis服务实体
    /// </summary>
    public class RedisServerModel
    {
        /// <summary>
        /// 是否启用Redis
        /// </summary>
        public bool Enable { get; set; }
        public string Cache { get; set; }
        public string Session { get; set; }
    }
}
