using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Dto;
using Xin.Service.TableColumn;
using Xin.Service.TableColumn.Dto;

namespace Xin.Admin.WebApi.Controllers
{
    public class TableColumnController: BaseController
    {
        private readonly ITableColumnService _tableColumnService;

        public TableColumnController(ITableColumnService tableColumnService)
        {
            _tableColumnService = tableColumnService;
        }
        /// <summary>
        /// 根据表标识获取当前用户的列
        /// </summary>
        /// <param name="tableMark">表标识</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResultOutput> GetTableColumnsByTableMark(string tableMark)
        {
            return await _tableColumnService.GetTableColumnsByTableMarkAsync(tableMark);
        }
        /// <summary>
        /// 新增表标识
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> AddTableColumn(List<TableColumnAddInput> inputs)
        {
            return await _tableColumnService.AddTableColumnAsync(inputs);
        }
        /// <summary>
        /// 修改表标识
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> EditTableColumn(List<TableColumnAddInput> inputs)
        {
            return await _tableColumnService.EditTableColumnAsync(inputs); 
        }
    }
}
