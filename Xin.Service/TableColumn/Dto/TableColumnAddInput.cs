using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.TableColumn.Dto
{
    public class TableColumnAddInput
    {
        /// <summary>
        /// 表标识
        /// </summary>
        [Required(ErrorMessage = "表标识不能为空")]
        public string TableMark { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        [Required(ErrorMessage = "列名不能为空")]
        public string ColumnName { get; set; }
        /// <summary>
        /// 列标题
        /// </summary>
        [Required(ErrorMessage = "列标题不能为空")]
        public string Label { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        public int Sort { get; set; }
        /// <summary>
        /// 是否展示
        /// </summary>
        [Required(ErrorMessage = "是否展示不能为空")]
        public bool IsShow { get; set; }

    }
}
