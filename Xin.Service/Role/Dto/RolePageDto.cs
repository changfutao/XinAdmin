using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.Role.Dto
{
    public class RolePageDto
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public int? Sort { get; set; }
    }
}
