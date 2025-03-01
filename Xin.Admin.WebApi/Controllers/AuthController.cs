using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xin.Infrastructure.Attributes;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Helpers;
using Xin.Infrastructure.Model;
using Xin.Model;
using Xin.Service.Auth.Dto;
using Xin.Service.Menu;
using Xin.Service.Menu.Dto;
using Xin.Service.User;
using Xin.Service.User.Dto;

namespace Xin.Admin.WebApi.Controllers
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    // [ApiExplorerSettings(GroupName = "认证", IgnoreApi = false)]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMenuService _menuService;

        public AuthController(IUserService userService, IHttpContextAccessor contextAccessor, IMenuService menuService)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
            _menuService = menuService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input">登录参数</param>
        /// <returns></returns>
        /// <remarks>
        ///   Sample request:
        ///     POST /api/Auth/Login
        ///     {
        ///       UserName: "xxx",
        ///       PassWord: "xxx"
        ///     }
        /// </remarks> 
        [HttpPost]
        [AllowAnonymous]
        [Login]
        // 声明控制器的操作支持application/json的相应内容类型
        [Produces("application/json")]
        public async Task<IResultOutput> Login(LoginDto input)
        {
            var user = await _userService.GetUserByNameAsync(input.UserName);
            if (user == null)
            {
                return ResultOutput.NotOk("用户名不存在");
            }
            if(!user.Password.Equals(input.Password))
            {
                return ResultOutput.NotOk("用户名或密码不正确");
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimAttributes.UserId, user.Id.ToString()),
                new Claim(ClaimAttributes.UserName, user.UserName),
                new Claim(ClaimAttributes.UserNickName, user.NickName ?? ""),
            };

            string token = JwtHelper.CreateToken(claims);
            return ResultOutput.Ok(token);
        }
        /// <summary>
        /// 获取用户信息和权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Login]
        public async Task<IResultOutput> GetUserInfoAndMenus()
        {
            var claims = _contextAccessor?.HttpContext?.User?.Claims;
            if (claims != null && claims.Any())
            {
                var claimUserId = claims.FirstOrDefault(a => a.Type == ClaimAttributes.UserId);
                if (claimUserId != null)
                {
                    var authUserInfo = new AuthUserInfoDto();
                    long id = claimUserId.Value.ToLong();
                    var user = await _userService.GetAsync(id);
                    if(user != null)
                    {
                        authUserInfo.UserName = user.UserName;
                        authUserInfo.NickName = user.NickName;
                    }
                    var userMenus = await _menuService.GetMenusByUserIdAsync(id);
                    authUserInfo.Menus = userMenus;
                    return ResultOutput.Ok(authUserInfo);
                }
            }
            return ResultOutput.NotOk("没有获取用户");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResultOutput> ChangePwd(ChangePwdDto input)
        {
            return await _userService.ChangePwdAsync(input);
        }
    }
}
