using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Entities
{
    public interface IEntityAdd<TKey> where TKey: struct
    {
        TKey? CreatedUserId { get; set; }
        string? CreatedUserName { get; set; }
        DateTime? CreatedTime { get; set; }
    }
}
