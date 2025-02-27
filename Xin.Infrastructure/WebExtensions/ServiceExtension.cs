using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Infrastructure.WebExtensions
{
    public static class ServiceExtension
    {
        public static void AddService(this IServiceCollection services)
        {
            // Service注入
            var types = Assembly.Load("Xin.Service")
                                        .GetExportedTypes()
                                        .Where(a => a.Name.EndsWith("Service"));
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                foreach (var baseType in interfaces)
                {
                    services.AddScoped(baseType, type);
                }
            }

        
        }
    }
}
