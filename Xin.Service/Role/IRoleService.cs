using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Service.Role.Dto;

namespace Xin.Service.Role
{
    public interface IRoleService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> GetPageAsync(PageInput<RolePageInput> input);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> AddAsync(RoleAddInput input);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IResultOutput> EditAsync(RoleEditInput input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultOutput> DeleteAsync(long[] ids);
        /// <summary>
        /// 根据Id获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultOutput> GetAsync(long id);
        /// <summary>
        /// 给角色设置权限
        /// </summary>
        /// <param name="rolePermission"></param>
        /// <returns></returns>
        Task<IResultOutput> SetPermission(RolePermissionDto rolePermission);
    }
}
