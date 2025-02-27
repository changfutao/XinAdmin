using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Model
{
    public class DbConfigs
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public string Conn { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public int DbType { get; set; }
        /// <summary>
        /// 多租户唯一标识
        /// </summary>
        public int ConfigId { get; set; }
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public bool IsAutoCloseConnection { get; set; }

    }

    public class SqlServerFlag { }
}
