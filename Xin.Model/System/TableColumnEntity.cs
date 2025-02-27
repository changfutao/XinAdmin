using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Entities;

namespace Xin.Model
{
    /// <summary>
    /// Table列
    /// </summary>
    [Table(Name = "sys_table_column")]
    public class TableColumnEntity: EntityFull
    {
        /// <summary>
        /// 标识名
        /// </summary>
        [Column(Position = 2, IsNullable = false, StringLength = 50)]
        [Description("表标识名")]
        public string TableMark { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(Position = 3, IsNullable = false, StringLength = 50)]
        [Description("用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        [Column(Position = 4, IsNullable = false, StringLength = 50)]
        [Description("列名")]
        public string ColumnName { get; set; }
        /// <summary>
        /// 列标题
        /// </summary>
        [Column(Position = 5, IsNullable = false, StringLength = 50)]
        [Description("列标题")]
        public string Label { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        [Column(Position = 6, IsNullable = false)]
        [Description("是否展示")]
        public bool IsShow { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column(Position =7, IsNullable = false)]
        [Description("排序")]
        public int Sort { get; set; }
    }
}
