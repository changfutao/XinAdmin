using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Model;
using Xin.Service.User.Dto;

namespace Xin.Service.User.Mapping
{
    public class UserMapRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // 0 正常 1 锁定 2 离职
            config.ForType<UserEntity, UserDto>()
                 .Map(dest => dest.Sex, src => src.Sex.HasValue ? (src.Sex.Value == 0 ? "男" : src.Sex.Value == 1 ? "女" : "未知") : "")
                 .Map(dest => dest.Status, src => src.Status == 0 ? "正常" : src.Status == 1 ? "锁定" : "离职");
        }
    }
}
