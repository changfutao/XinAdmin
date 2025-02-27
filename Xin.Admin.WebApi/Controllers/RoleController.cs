using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure;
using Xin.Infrastructure.Dto;
using Xin.Service.Role;
using Xin.Service.Role.Dto;

namespace Xin.Admin.WebApi.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [ApiExplorerSettings(GroupName = "角色管理")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetPage(PageInput<RolePageInput> input)
        {
            return await _roleService.GetPageAsync(input);
        }
        /// <summary>
        /// 根据Id获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultOutput> Get(long id)
        {
            return await _roleService.GetAsync(id);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> Add(RoleAddInput input)
        {
            return await _roleService.AddAsync(input);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> Edit(RoleEditInput input)
        {
            return await _roleService.EditAsync(input);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultOutput> Delete(long[] ids)
        {
            return await _roleService.DeleteAsync(ids);
        }
    }
 }
