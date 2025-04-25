using Xin.Infrastructure.Dto;
using Xin.Service.Department.Dto;

namespace Xin.Service.Department;

public interface IDepartmentService
{
    Task<IResultOutput> GetDepartmentTreeData(DeptInput input);
    Task<IResultOutput> AddDept(DeptAddInput input);
    Task<IResultOutput> EditDept(DeptEditInput input);
    Task<IResultOutput> DeleteDept(long id);
}