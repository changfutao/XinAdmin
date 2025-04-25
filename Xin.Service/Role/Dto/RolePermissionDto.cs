namespace Xin.Service.Role.Dto;

public class RolePermissionDto
{
    // 角色Id
    public long RoleId { get; set; }
    // 权限Id
    public List<long>? PermissionIds { get; set; }
}