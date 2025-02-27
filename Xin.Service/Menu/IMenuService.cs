using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Service.Menu.Dto;

namespace Xin.Service.Menu
{
    public interface IMenuService
    {
        /// <summary>
        /// 根据用户Id获取菜单列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(List<MenuDto>, List<MenuDto>)> GetMenusByUserIdAsync(long id);
    }
}
