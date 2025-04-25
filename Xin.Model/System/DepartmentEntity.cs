using System.ComponentModel;
using FreeSql.DataAnnotations;
using Xin.Infrastructure.Entities;

namespace Xin.Model;
[Table(Name = "sys_dept")]
public class DepartmentEntity: EntityFull
{
    /// <summary>
    /// 部门名称
    /// </summary>
    [Description("部门名称")]
    [Column(Position = 2, StringLength = 30, IsNullable = false)]
    public string DeptName { get; set; }
    /// <summary>
    /// 父级部门Id
    /// </summary>
    [Description("父级部门Id")]
    [Column(Position = 3)]
    public long? ParentId { get; set; }
    /// <summary>
    /// 部门负责人Id 
    /// </summary>
    [Description("部门负责人Id")]
    [Column(Position = 4)]
    public long? LeaderId { get; set; }
}