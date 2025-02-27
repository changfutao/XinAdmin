using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Entities
{
    public interface IEntityUpdate<TKey> where TKey : struct
    {
        TKey? ModifiedUserId { get; set; }
        string? ModifiedUserName { get; set; }
        DateTime? ModifiedTime { get; set; }
    }
}
