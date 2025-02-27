using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Entities
{
    public interface IEntitySoftDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
