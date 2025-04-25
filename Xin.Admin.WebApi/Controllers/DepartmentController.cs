using Microsoft.AspNetCore.Mvc;
using Xin.Infrastructure.Dto;
using Xin.Service.Department;
using Xin.Service.Department.Dto;

namespace Xin.Admin.WebApi.Controllers;
/// <summary>
/// 部门
/// </summary>
public class DepartmentController : BaseController
{
    private readonly IDepartmentService _deptService;

    public DepartmentController(IDepartmentService deptService)
    {
        _deptService = deptService;
    }
    /// <summary>
    /// 获取部门树数据
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultOutput> GetDepartmentTreeData(DeptInput input)
    {
        return _deptService.GetDepartmentTreeData(input);
    }
    /// <summary>
    /// 新增部门
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultOutput> AddDept(DeptAddInput input)
    {
        return _deptService.AddDept(input);
    }
    /// <summary>
    /// 编辑部门
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultOutput> EditDept(DeptEditInput input)
    {
        return _deptService.EditDept(input);
    }
    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultOutput> DeleteDept(long id)
    {
        return _deptService.DeleteDept(id);
    }
}