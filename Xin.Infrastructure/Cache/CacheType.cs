using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Cache
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 内存
        /// </summary>
        Memory,
        /// <summary>
        /// Redis
        /// </summary>
        Redis
    }
}
