using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Entities
{
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        TKey Id { get; set; }
    }

    public interface IEntity: IEntity<long>
    {

    }

    public class Entity<TKey> : IEntity<TKey>
    {
        [Description("主键Id")]
        [Column(Position = 1, IsIdentity = false, IsPrimary = true)]
        public virtual TKey Id { get; set; }
    }

    public class Entity: Entity<long>
    {

    }
}
