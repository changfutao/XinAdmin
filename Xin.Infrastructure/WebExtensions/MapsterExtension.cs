using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.WebExtensions
{
    /// <summary>
    /// Mapster注入
    /// </summary>
    public static class MapsterExtension
    {
        public static void AddMapster(this IServiceCollection services, Action<TypeAdapterConfig> options = null)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.Load("Xin.Service"));
            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);
        }
    }
}
