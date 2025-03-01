using FreeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Xin.Infrastructure.WebExtensions
{
    /// <summary>
    /// Redis服务注入
    /// </summary>
    public static class RedisExtension
    {
        public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisServerModel = configuration.GetSection("RedisServer").Get<RedisServerModel>();
            if(redisServerModel == null)
            {
                throw new ArgumentNullException("Redis Paramenter Is Not Exist");
            }
            // Redis启用的话注入服务
            if (redisServerModel.Enable)
            {
                RedisClient cli = new RedisClient(redisServerModel.Cache);
                cli.Notice += (s, e) => Console.WriteLine(e.Log);
                services.AddSingleton(sp => cli);
            }
        }
    }
}
