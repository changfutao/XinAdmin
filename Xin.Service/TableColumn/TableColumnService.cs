using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Attributes;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.TableColumn.Dto;

namespace Xin.Service.TableColumn
{
    public class TableColumnService: ITableColumnService
    {
        private readonly IFreeSql<SqlServerFlag> _fsql;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableColumnService(IFreeSql<SqlServerFlag> fsql, IHttpContextAccessor httpContextAccessor)
        {
            _fsql = fsql;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 根据表标识获取当前用户的列
        /// </summary>
        /// <param name="tableMark">表标识</param>
        /// <returns></returns>
        public async Task<IResultOutput> GetTableColumnsByTableMarkAsync(string tableMark)
        {
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            if(claims == null)
            {
                return ResultOutput.NotOk("没有获取用户");
            }
            var claim = claims.FirstOrDefault(a => a.Type == ClaimAttributes.UserName);
            if(claim == null)
            {
                return ResultOutput.NotOk("Claim中没有获取用户");
            }
            var tableColumns = await _fsql.Select<TableColumnEntity>()
                  .Where(a => a.TableMark == tableMark && a.UserName == claim.Value)
                  .OrderBy(a => a.Sort)
                  .ToListAsync();
            var tableColumnDtos = tableColumns.Adapt<List<TableColumnDto>>();
            return ResultOutput.Ok(tableColumnDtos);
        }
        /// <summary>
        /// 添加Table列关系
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public async Task<IResultOutput> AddTableColumnAsync(List<TableColumnAddInput> inputs)
        {
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            if (claims == null)
            {
                return ResultOutput.NotOk("没有获取用户");
            }
            var claim = claims.FirstOrDefault(a => a.Type == ClaimAttributes.UserName);
            if (claim == null)
            {
                return ResultOutput.NotOk("Claim中没有获取用户");
            }
            var tableColumns = inputs.Adapt<List<TableColumnEntity>>();
            foreach (var item in tableColumns)
            {
                item.UserName = claim.Value;
            }
            await _fsql.Insert(tableColumns).ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }

        /// <summary>
        /// 编辑Table列关系
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public async Task<IResultOutput> EditTableColumnAsync(List<TableColumnAddInput> inputs)
        {
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            if (claims == null)
            {
                return ResultOutput.NotOk("没有获取用户");
            }
            var claim = claims.FirstOrDefault(a => a.Type == ClaimAttributes.UserName);
            if (claim == null)
            {
                return ResultOutput.NotOk("Claim中没有获取用户");
            }
            await _fsql.Delete<TableColumnEntity>()
                 .Where(a => a.TableMark == inputs.First().TableMark && a.UserName == claim.Value)
                 .ExecuteAffrowsAsync();
            var tableColumns = inputs.Adapt<List<TableColumnEntity>>();
            foreach (var item in tableColumns)
            {
                item.UserName = claim.Value;
            }
            await _fsql.Insert(tableColumns).ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }
    }
}
