using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.Role.Dto;

namespace Xin.Service.Role
{
    public class RoleService : IRoleService
    {
        private readonly IFreeSql<SqlServerFlag> _fsql;

        public RoleService(IFreeSql<SqlServerFlag> fsql)
        {
            _fsql = fsql;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetPageAsync(PageInput<RolePageInput> input)
        {
            var roles = await _fsql.Select<RoleEntity>()
                .WhereIf(!string.IsNullOrEmpty(input.Filter?.Name), a => a.Name.Contains(input.Filter.Name))
                .Count(out long total)
                .Skip((input.CurrentPage - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();
            return ResultOutput.Ok(new { list = roles.Adapt<List<RolePageDto>>(), total = total });
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> AddAsync(RoleAddInput input)
        {
            var role = input.Adapt<RoleEntity>();
            await _fsql.Insert(role).ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IResultOutput> EditAsync(RoleEditInput input)
        {
            var role = await _fsql.Select<RoleEntity>().Where(a => a.Id == input.Id).FirstAsync();
            if (role == null)
            {
                return ResultOutput.NotOk("角色不存在");
            }

            var roleModel = input.Adapt<RoleEntity>();
            await _fsql.Update<RoleEntity>()
                .SetSource(roleModel)
                .ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IResultOutput> DeleteAsync(long[] ids)
        {
            await _fsql.Update<RoleEntity>()
                .Set(a => a.IsDeleted, true)
                .Where(a => ids.Contains(a.Id))
                .ExecuteAffrowsAsync();
            return ResultOutput.Ok();
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResultOutput> GetAsync(long id)
        {
            var role = await _fsql.Select<RoleEntity>()
                .Where(a => a.Id == id)
                .FirstAsync();
            if (role == null)
            {
                return ResultOutput.NotOk("角色不存在");
            }

            return ResultOutput.Ok(role);
        }

        /// <summary>
        /// 给角色设置权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="permissionIds">权限Id集合</param>
        /// <returns></returns>
        public async Task<IResultOutput> SetPermission(RolePermissionDto rolePermission)
        {
            await _fsql.Delete<RoleMenuEntity>()
                .Where(a => a.RoleId == rolePermission.RoleId)
                .ExecuteAffrowsAsync();
            if (rolePermission.PermissionIds != null)
            {
                List<RoleMenuEntity> roleMenuList = new List<RoleMenuEntity>();
                foreach (var permissionId in rolePermission.PermissionIds)
                {
                    RoleMenuEntity roleMenu = new RoleMenuEntity();
                    roleMenu.RoleId = rolePermission.RoleId;
                    roleMenu.MenuId = permissionId;
                    roleMenu.CreatedTime = DateTime.Now;
                    roleMenuList.Add(roleMenu);
                }
                await _fsql.Insert(roleMenuList).ExecuteAffrowsAsync();
            }
            return ResultOutput.Ok();
        }
    }
}