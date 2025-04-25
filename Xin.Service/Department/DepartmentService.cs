using Mapster;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.Department.Dto;

namespace Xin.Service.Department;

public class DepartmentService : IDepartmentService
{
    private readonly IFreeSql<SqlServerFlag> _fsql;

    public DepartmentService(IFreeSql<SqlServerFlag> fsql)
    {
        _fsql = fsql;
    }

    /// <summary>
    /// 获取部门树数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> GetDepartmentTreeData(DeptInput input)
    {
        var depts = await _fsql.Select<DepartmentEntity, UserEntity>()
            .LeftJoin((a, b) => a.LeaderId == b.Id)
            .ToListAsync((a, b) => new DeptDto
            {
                DeptName = a.DeptName,
                CreatedTime = a.CreatedTime,
                ParentId = a.ParentId,
                LeaderId = a.LeaderId,
                LeaderName = b.UserName
            });
        if (!string.IsNullOrEmpty(input.DeptName))
        {
            var deptsFilter = depts.Where(a => a.DeptName.Contains(input.DeptName));
            List<DeptDto> deptList = new List<DeptDto>();
            foreach (var dept in deptsFilter)
            {
                if (!deptList.Any(a => a.Id == dept.Id))
                {
                    deptList.Add(dept);
                }

                GetDeptTreeByName(depts, dept, deptList);
            }

            depts = deptList;
        }

        var list = GenerateDeptTree(depts, null);
        return ResultOutput.Ok(list);
    }

    /// <summary>
    /// 所有过滤后的部门数据
    /// </summary>
    /// <param name="all"></param>
    /// <param name="dept"></param>
    /// <returns></returns>
    private void GetDeptTreeByName(List<DeptDto> all, DeptDto dept, List<DeptDto> deptList)
    {
        // 获取当前元素的父级
        var parent = all.FirstOrDefault(a => a.Id == dept.ParentId);
        if (parent != null)
        {
            if (!deptList.Any(a => a.Id == parent.Id))
            {
                deptList.Add(parent);
            }
            GetDeptTreeByName(all, parent, deptList);
        }
    }

    /// <summary>
    /// 生成部门树
    /// </summary>
    /// <param name="depts">部门集合</param>
    /// <param name="pid">父级Id</param>
    /// <returns></returns>
    private List<DeptDto> GenerateDeptTree(List<DeptDto> depts, long? pid)
    {
        List<DeptDto> deptList = new();
        var deptsChildren = depts.Where(a => a.ParentId == pid).ToList();

        if (deptsChildren.Any())
        {
            foreach (var dept in deptsChildren)
            {
                var children = GenerateDeptTree(depts, dept.Id);
                if (children.Any())
                {
                    dept.Children = children;
                }

                deptList.Add(dept);
            }
        }

        return deptList;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> AddDept(DeptAddInput input)
    {
        // 同一层级下部门名称不能重复
        if (await _fsql.Select<DepartmentEntity>()
                .AnyAsync(a => a.ParentId == input.ParentId && a.DeptName == input.DeptName))
        {
            return ResultOutput.NotOk("同一层级下部门名称不能重复");
        }

        var deptModel = input.Adapt<DepartmentEntity>();
        await _fsql.Insert(deptModel).ExecuteAffrowsAsync();
        return ResultOutput.Ok();
    }

    /// <summary>
    /// 编辑部门
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<IResultOutput> EditDept(DeptEditInput input)
    {
        // 同一层级下部门名称不能重复
        if (await _fsql.Select<DepartmentEntity>()
                .AnyAsync(a => a.Id != input.Id && a.ParentId == input.ParentId && a.DeptName == input.DeptName))
        {
            return ResultOutput.NotOk("同一层级下部门名称不能重复");
        }

        var deptModel = input.Adapt<DepartmentEntity>();
        await _fsql.Update<DepartmentEntity>()
            .SetSource(deptModel)
            .ExecuteAffrowsAsync();
        return ResultOutput.Ok();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResultOutput> DeleteDept(long id)
    {
        await _fsql.Update<DepartmentEntity>()
            .Set(a => a.IsDeleted, true)
            .Where(a => a.Id == id)
            .ExecuteAffrowsAsync();
        return ResultOutput.Ok();
    }
}