using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Service.TableColumn.Dto;

namespace Xin.Service.TableColumn
{
    public interface ITableColumnService
    {
        /// <summary>
        /// 根据表标识获取当前用户的列
        /// </summary>
        /// <param name="tableMark">表标识</param>
        /// <returns></returns>
        Task<IResultOutput> GetTableColumnsByTableMarkAsync(string tableMark);
        /// <summary>
        /// 添加Table列关系
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task<IResultOutput> AddTableColumnAsync(List<TableColumnAddInput> inputs);
        /// <summary>
        /// 编辑Table列关系
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task<IResultOutput> EditTableColumnAsync(List<TableColumnAddInput> inputs);
    }
}
