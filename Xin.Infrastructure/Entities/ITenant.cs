using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Entities
{
    public interface ITenant<TKey> where TKey: struct
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        TKey? TenantId { get; set; }
    }
}
