
using Xin.Service.User;

namespace Xin.Admin.WebApi.Auth
{
    public class PermissionHandler : IPermissionHandler
    {
        private readonly IUserService _userService;

        public PermissionHandler(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="api">接口路径</param>
        /// <returns></returns>
        public async Task<bool> ValidateAsync(string api, long userId)
        {
            // 查询当前用户的菜单权限
            //var userMenu = await _userService.GetUserMenusAsync(userId);
            //if(!userMenu.Menus.Any(a => a.Path == api))
            //{
            //    return false;
            //}
            return true;
        }
    }
}
