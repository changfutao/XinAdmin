namespace Xin.Service.Department.Dto;

public class DeptDto
{
    public long Id { get; set; }
    public string DeptName { get; set; }
    public long? ParentId { get; set; }
    public long? LeaderId { get; set; }
    public string? LeaderName { get; set; }
    public DateTime? CreatedTime { get; set; }
    public List<DeptDto>? Children { get; set; }
}