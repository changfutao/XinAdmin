
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
        public Task<bool> ValidateAsync(string api)
        {
            return Task.FromResult(true);
        }
    }
}
