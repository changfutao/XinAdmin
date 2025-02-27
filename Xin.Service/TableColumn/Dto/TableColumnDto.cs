using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.TableColumn.Dto
{
    public class TableColumnDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 列标题
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow { get; set; }
    }
}
