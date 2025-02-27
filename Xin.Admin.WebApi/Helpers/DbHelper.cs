﻿using FreeSql;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using Xin.Infrastructure.Entities;
using Xin.Infrastructure.Model;
using Xin.Model;
using Yitter.IdGenerator;

namespace Xin.Admin.WebApi.Helpers
{
    /// <summary>
    /// 数据库同步
    /// </summary>
    public class DbHelper
    {
        public static void InitDb(IConfiguration configuration)
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

                        var fsql1 = new FreeSqlBuilder()
                                                        .UseConnectionString(DataType.SqlServer, dbConfig.Conn)
                                                        .Build<SqlServerFlag>();
                        // 创建数据库
                        CreateDatabaseIfNotExistsSqlServer(dbConfig.Conn);
                        Console.WriteLine($"\r\n 同步{dbConfig.Name}");
                        var types = Assembly.GetAssembly(typeof(UserEntity)).GetExportedTypes().Where(a => a.Name.EndsWith("Entity")).ToArray();
                        // fsql1.CodeFirst.IsAutoSyncStructure = true;
                        fsql1.CodeFirst.SyncStructure(types);
                        Console.WriteLine("同步完成");
                        InitBaseData(fsql1);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void CreateDatabaseIfNotExistsSqlServer(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            string createDatabaseSql;
            if (!string.IsNullOrEmpty(builder.AttachDBFilename))
            {
                string fileName = ExpandFileName(builder.AttachDBFilename);
                string name = Path.GetFileNameWithoutExtension(fileName);
                string logFileName = Path.ChangeExtension(fileName, ".ldf");
                createDatabaseSql = @$"CREATE DATABASE {builder.InitialCatalog}   on  primary   
                (
                    name = '{name}',
                    filename = '{fileName}'
                )
                log on
                (
                    name= '{name}_log',
                    filename = '{logFileName}'
                )";
            }
            else
            {
                createDatabaseSql = @$"CREATE DATABASE {builder.InitialCatalog}";
            }

            using SqlConnection cnn = new SqlConnection($"Data Source={builder.DataSource};Integrated Security = false;User ID={builder.UserID};Password={builder.Password};Initial Catalog=master;Min pool size=1");
            cnn.Open();
            using SqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = $"select * from sysdatabases where name = '{builder.InitialCatalog}'";

            SqlDataAdapter apter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            apter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                cmd.CommandText = createDatabaseSql;
                cmd.ExecuteNonQuery();
            }
        }

        private static string ExpandFileName(string fileName)
        {
            if (fileName.StartsWith("|DataDirectory|", StringComparison.OrdinalIgnoreCase))
            {
                var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
                if (string.IsNullOrEmpty(dataDirectory))
                {
                    dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
                }
                string name = fileName.Replace("\\", "").Replace("/", "").Substring("|DataDirectory|".Length);
                fileName = Path.Combine(dataDirectory, name);
            }
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            return Path.GetFullPath(fileName);
        }

        private static void InitBaseData(IFreeSql fsql)
        {
            var userExist = fsql.Select<UserEntity>()
                 .Any();
            long userId = 0;
            if(!userExist)
            {
                UserEntity user = new UserEntity();
                user.UserName = "admin";
                user.NickName = "admin";
                user.Sex = 0;
                user.ErrorCount = 0;
                user.Status = 0;
                user.Password = "123456";
                fsql.Insert(user).ExecuteAffrows();
                userId = user.Id;
            }
            var roleExist = fsql.Select<RoleEntity>().Any();
            long roleId = 0;                        
            if(!roleExist)
            {
                RoleEntity role = new RoleEntity();
                role.Name = "管理员";
                role.Code = "admin";
                role.Sort = 1;
                fsql.Insert(role).ExecuteAffrows();
                roleId = role.Id;
            }

            var userRoleExist = fsql.Select<UserRoleEntity>().Any();
            if(!userRoleExist)
            {
                UserRoleEntity userRole = new UserRoleEntity();
                userRole.UserId = userId;
                userRole.RoleId = roleId;
                fsql.Insert(userRole).ExecuteAffrows();
            }
            var menuExist = fsql.Select<MenuEntity>().Any();
            if(!menuExist)
            {
                List<MenuEntity> menus = new List<MenuEntity>();
                MenuEntity menu1 = new MenuEntity();
                menu1.MenuName = "系统管理";
                menu1.IsFrame = false;
                menu1.IsVisible = true;
                menu1.MenuType = 0;
                menu1.ParentId = 0;
                menu1.Sort = 1;
                menus.Add(menu1);
                MenuEntity menu2 = new MenuEntity();
                menu2.MenuName = "用户管理";
                menu2.IsFrame = false;
                menu2.IsVisible = true;
                menu2.MenuType = 1;
                menu2.ParentId = menu1.Id;
                menu2.Sort = 1;
                menu2.FrontName = "user";
                menu2.FrontRoutePath = "/user";
                menu2.FrontPath = "../views/user/index.vue";
                menu2.Path = "/api/user/getpage";
                menus.Add(menu2);
                MenuEntity menu3 = new MenuEntity();
                menu3.MenuName = "角色管理";
                menu3.IsFrame = false;
                menu3.IsVisible = true;
                menu3.MenuType = 1;
                menu3.ParentId = menu1.Id;
                menu3.Sort = 1;
                menu3.FrontName = "role";
                menu3.FrontRoutePath = "/role";
                menu3.FrontPath = "../views/role/index.vue";
                menu3.Path = "/api/role/getpage";
                menus.Add(menu3);
                MenuEntity menu4 = new MenuEntity();
                menu4.MenuName = "菜单管理";
                menu4.IsFrame = false;
                menu4.IsVisible = true;
                menu4.MenuType = 1;
                menu4.ParentId = menu1.Id;
                menu4.Sort = 1;
                menu4.FrontName = "menu";
                menu4.FrontRoutePath = "/menu";
                menu4.FrontPath = "../views/menu/index.vue";
                menu4.Path = "/api/menu/getpage";
                menus.Add(menu4);
                MenuEntity menu5 = new MenuEntity();
                menu5.MenuName = "添加";
                menu5.IsFrame = false;
                menu5.IsVisible = true;
                menu5.MenuType = 2;
                menu5.ParentId = menu2.Id;
                menu5.Sort = 1;
                menu5.FrontName = "";
                menu5.FrontRoutePath = "";
                menu5.FrontPath = "";
                menu5.Path = "/api/user/add";
                menu5.Perms = "api:user:add";
                menus.Add(menu5);
                MenuEntity menu6 = new MenuEntity();
                menu6.MenuName = "编辑";
                menu6.IsFrame = false;
                menu6.IsVisible = true;
                menu6.MenuType = 2;
                menu6.ParentId = menu2.Id;
                menu6.Sort = 1;
                menu6.FrontName = "";
                menu6.FrontRoutePath = "";
                menu6.FrontPath = "";
                menu6.Path = "/api/user/edit";
                menu6.Perms = "api:user:edit";
                menus.Add(menu6);
                MenuEntity menu7 = new MenuEntity();
                menu7.MenuName = "删除";
                menu7.IsFrame = false;
                menu7.IsVisible = true;
                menu7.MenuType = 2;
                menu7.ParentId = menu2.Id;
                menu7.Sort = 1;
                menu7.FrontName = "";
                menu7.FrontRoutePath = "";
                menu7.FrontPath = "";
                menu7.Path = "/api/user/delete";
                menu7.Perms = "api:user:delete";
                menus.Add(menu7);
                fsql.Insert(menus).ExecuteAffrows();
                // 角色菜单
                List<RoleMenuEntity> roleMenus = new List<RoleMenuEntity>();
                RoleMenuEntity rm1 = new RoleMenuEntity();
                rm1.RoleId = roleId;
                rm1.MenuId = menu1.Id;
                roleMenus.Add(rm1);
                RoleMenuEntity rm2 = new RoleMenuEntity();
                rm2.RoleId = roleId;
                rm2.MenuId = menu2.Id;
                roleMenus.Add(rm2);
                RoleMenuEntity rm3 = new RoleMenuEntity();
                rm3.RoleId = roleId;
                rm3.MenuId = menu3.Id;
                roleMenus.Add(rm3);
                RoleMenuEntity rm4 = new RoleMenuEntity();
                rm4.RoleId = roleId;
                rm4.MenuId = menu4.Id;
                roleMenus.Add(rm4);
                RoleMenuEntity rm5 = new RoleMenuEntity();
                rm5.RoleId = roleId;
                rm5.MenuId = menu5.Id;
                roleMenus.Add(rm5);
                RoleMenuEntity rm6 = new RoleMenuEntity();
                rm6.RoleId = roleId;
                rm6.MenuId = menu6.Id;
                roleMenus.Add(rm6);
                RoleMenuEntity rm7 = new RoleMenuEntity();
                rm7.RoleId = roleId;
                rm7.MenuId = menu7.Id;
                roleMenus.Add(rm7);
                fsql.Insert(roleMenus).ExecuteAffrows();
            }
            Console.WriteLine("基础数据初始化完成");
        }
    }
}
