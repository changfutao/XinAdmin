using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Entities;
using Xin.Infrastructure.Model;

namespace Xin.Infrastructure.WebExtensions
{
    /// <summary>
    /// 数据库帮助扩展类
    /// </summary>
    public static class DBExtension
    {
        public static void AddFreeSql(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConfigs = configuration.GetSection("DbConfigs").Get<List<DbConfigs>>();
            if (!dbConfigs.Any())
            {
                throw new ArgumentNullException(nameof(DbConfigs));
            }
            foreach (var dbConfig in dbConfigs)
            {
                switch (dbConfig.Name)
                {
                    case "SqlServerFlag":
                        Func<IServiceProvider, IFreeSql<SqlServerFlag>> fsql1 = r =>
                        {
                            var fsql = new FreeSqlBuilder()
                                                            .UseConnectionString(DataType.SqlServer, dbConfig.Conn)
                                                            .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
                                                            .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                                                            .Build<SqlServerFlag>();
                            fsql.GlobalFilter.Apply<IEntitySoftDelete>("SoftDelete", a => a.IsDeleted == false);
                            return fsql;
                        };
                        services.AddSingleton<IFreeSql<SqlServerFlag>>(fsql1);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
