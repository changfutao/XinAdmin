using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Service.Menu.Dto;

namespace Xin.Service.Auth.Dto
{
    public class AuthUserInfoDto
    {
        public string? UserName { get; set; }
        public string? NickName { get; set; }
        public List<MenuDto>? Menus { get; set; }
    }
}
