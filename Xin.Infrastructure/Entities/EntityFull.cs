using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;
using Yitter.IdGenerator;

namespace Xin.Infrastructure.Entities
{
    /// <summary>
    /// 完整实体类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EntityFull<TKey> : Entity<TKey>, IEntitySoftDelete, IEntityAdd<TKey>, IEntityUpdate<TKey> where TKey: struct
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        [Description("是否删除")]
        [Column(Position = -8)]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 创建者Id
        /// </summary>
        [Description("创建者Id")]
        [Column(Position = -7, CanUpdate = false)]
        public TKey? CreatedUserId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Description("创建者")]
        [Column(Position = -6, CanUpdate = false), MaxLength(50)]
        public string? CreatedUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        [Column(Position = -5, CanUpdate = false, ServerTime = DateTimeKind.Local)]
        public DateTime? CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改者Id
        /// </summary>
        [Description("修改者Id")]
        [Column(Position = -4, CanInsert = false)]
        public TKey? ModifiedUserId { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [Description("修改者")]
        [Column(Position = -2, CanInsert = false), MaxLength(50)]
        public string? ModifiedUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("修改时间")]
        [Column(Position = -1, CanInsert = false, ServerTime = DateTimeKind.Local)]
        public DateTime? ModifiedTime { get; set; }

    }

    public class EntityFull: EntityFull<long>
    {
        [Description("主键Id")]
        [Column(Position = 1, IsIdentity = false, IsPrimary = true)]
        public new long Id { get; set; } = YitIdHelper.NextId();
    }
}
