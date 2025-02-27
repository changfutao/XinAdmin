using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Xin.Infrastructure.WebExtensions
{
    /// <summary>
    /// Cors扩展类
    /// </summary>
    public static class CorsExtension
    {
        public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsUrls = configuration.GetSection("CorsUrls").Get<string[]>();

            // 配置跨域
            return services.AddCors(options =>
            {
                options.AddPolicy("Policy", policy =>
                {
                    policy.WithOrigins(corsUrls ?? Array.Empty<string>())
                          .AllowAnyHeader() // 允许任意头
                          .AllowCredentials() // 允许cookie
                          .AllowAnyMethod(); //允许任意方法
                });
            });
        }
    }
}
