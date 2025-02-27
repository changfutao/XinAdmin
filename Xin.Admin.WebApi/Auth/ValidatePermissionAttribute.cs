using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Xin.Admin.WebApi.Auth;

namespace Xin.Infrastructure.Attributes
{
    /// <summary>
    /// 启用权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ValidatePermissionAttribute : AuthorizeAttribute, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        private async Task PermissionAuthorization(AuthorizationFilterContext context)
        {
            // 排除匿名访问
            if(context.ActionDescriptor.EndpointMetadata.Any(e => e.GetType() == typeof(AllowAnonymousAttribute)))
            {
                return;
            }

            // 登录验证
            var user = context.HttpContext.User.FindFirst(ClaimAttributes.UserId);
            if(user == null)
            {
                context.Result = new ChallengeResult();
                return;
            }

            // 排除登录接口
            if(context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType() == typeof(LoginAttribute)))
            {
                return;
            }

            // 权限验证
            var httpMethod = context.HttpContext.Request.Method;
            // 请求的url例如: api/auth/get
            var api = context.ActionDescriptor.AttributeRouteInfo.Template;
            // 判断当前用户是否具有此权限
            var permissionHandler = context.HttpContext.RequestServices.GetService<IPermissionHandler>();
            var isValid = await permissionHandler.ValidateAsync(api);
            if(!isValid)
            {
                context.Result = new ForbidResult();
            }
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            await PermissionAuthorization(context);
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await PermissionAuthorization(context);
        }
    }
}
