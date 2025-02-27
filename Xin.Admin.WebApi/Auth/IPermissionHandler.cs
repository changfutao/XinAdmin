namespace Xin.Admin.WebApi.Auth
{
    public interface IPermissionHandler
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        Task<bool> ValidateAsync(string api);
    }
}
