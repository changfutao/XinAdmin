
using Xin.Service.Menu.Dto;

namespace Xin.Service.Role.Dto;

public class RoleMenuTreeDto
{
    public List<MenuDto> Menus { get; set; }
    public List<long> MenuIds { get; set; }
}