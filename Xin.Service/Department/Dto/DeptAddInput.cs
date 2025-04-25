using System.ComponentModel.DataAnnotations;

namespace Xin.Service.Department.Dto;

public class DeptAddInput
{
    [Required(ErrorMessage = "部门名称必填")]
    public string DeptName { get; set; }
    public long? ParentId { get; set; }
    public long? LeaderId { get; set; }
}