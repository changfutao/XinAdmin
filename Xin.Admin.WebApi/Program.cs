using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Xin.Admin.WebApi.Auth;
using Xin.Admin.WebApi.Helpers;
using Xin.Infrastructure;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Filters;
using Xin.Infrastructure.Helpers;
using Xin.Infrastructure.WebExtensions;
using Yitter.IdGenerator;
namespace Xin.Admin.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        // Add services to the container.
        var types = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.Name.EndsWith("Controller")).ToList();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "XinAdmin.WebAPi",
                Description = "XinAdmin的后台管理系统",
            });

          
            types.ForEach(a =>
            {
                var apiExplorerSettingsAttribute = a.GetCustomAttribute<ApiExplorerSettingsAttribute>();
                if (apiExplorerSettingsAttribute != null && !string.IsNullOrEmpty(apiExplorerSettingsAttribute.GroupName))
                {
                    options.SwaggerDoc(apiExplorerSettingsAttribute.GroupName, new OpenApiInfo
                    {
                        Version = apiExplorerSettingsAttribute.GroupName
                    });
                }
            });

            var controllerFileName = "Xin.Admin.WebApi.xml";
            // 控制器注释
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, controllerFileName), true);

            // Service注释
            var serviceFileName = "Xin.Service.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, serviceFileName));

            // 定义JwtBearer认证
            options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme
            {
                Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
                Name = "Authorization",//jwt默认的参数名称
                In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            //定义JwtBearer认证方式二
            //options.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
            //{
            //    Description = "这是方式二(JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）)",
            //    Name = "Authorization",//jwt默认的参数名称
            //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
            //    Type = SecuritySchemeType.ApiKey
            //});

            //声明一个Scheme，注意下面的Id要和上面AddSecurityDefinition中的参数name一致
            var scheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "JwtBearer" }
            };
            //注册全局认证（所有的接口都可以使用认证）
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                [scheme] = new string[0]
            });
        });

        #region Mapster
        builder.Services.AddMapster(options =>
        {
            options.Default.IgnoreNonMapped(true); // Does not work.
            TypeAdapterConfig.GlobalSettings.Default.IgnoreNonMapped(true); // Does not work.
        });
        #endregion

        #region 注入HttpContextAccessor
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        #endregion

        #region 注入Cors
        builder.Services.AddCors(configuration);
        #endregion

        #region 注入Redis
        builder.Services.AddRedis(configuration);
        #endregion

        #region AppSettings
        builder.Services.AddSingleton(new AppSettings(configuration));
        #endregion

        #region 雪花算法
        // 创建 IdGeneratorOptions 对象，可在构造函数中输入 WorkerId：
        var options = new IdGeneratorOptions(1) { WorkerIdBitLength = 6 };
        // 保存参数（务必调用，否则参数设置不生效）：
        YitIdHelper.SetIdGenerator(options);
        #endregion

        #region 数据库
        builder.Services.AddFreeSql(configuration);
        DbHelper.InitDb(configuration);
        #endregion

        #region 认证
        // 使用Jwt Bearer认证
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   options.TokenValidationParameters = new()
                   {
                       // 有效的签名算法列表，仅列出的算法是有效的（强烈建议不要设置该属性）
                       // 默认 null，即所有算法均可
                       // ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256, SecurityAlgorithms.RsaSha256 },
                       // 有效的token类型
                       // 默认 null，即所有类型均可
                       // ValidTypes = new[] { JwtConstants.HeaderType },
                       // 是否验证颁发者
                       ValidateIssuer = true,
                       // 颁发者
                       ValidIssuer = jwtSettings.Issuer,
                       // 是否验证订阅者
                       ValidateAudience = true,
                       // 订阅者
                       ValidAudience = jwtSettings.Audience,
                       // 是否验证token是否在有效期内，即验证Jwt的Payload部分的nbf和exp
                       ValidateLifetime = true,
                       // 设置时钟漂移，可以在验证token有效期时，允许一定的时间误差（如时间刚达到token中exp，但是允许未来5分钟内该token仍然有效）。默认为300s，即5min。本例jwt的签发和验证均是同一台服务器，所以这里就不需要设置时钟漂移了。
                       ClockSkew = TimeSpan.Zero,
                       // 签发者用于Token签名的密钥
                       // 对称加密,使用相同的key进行加签验签
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                   };

                   options.Events = new JwtBearerEvents
                   {
                       OnTokenValidated = context =>
                       {
                           return Task.CompletedTask;
                       }
                   };
               });
        #endregion

        #region 授权
        builder.Services.AddScoped<IPermissionHandler, PermissionHandler>();
        #endregion

        #region 服务注入
        builder.Services.AddService();
        #endregion

        #region 控制器
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<GlobalExceptionFilter>();
        });
        #endregion

        #region 对模型验证进行自定义统一的返回格式
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                // 获取验证失败时的模型字段的错误消息
                var errors = actionContext.ModelState
                         .Where(a => a.Value?.Errors?.Count > 0)
                         .Select(a => a.Value?.Errors.FirstOrDefault()?.ErrorMessage)
                         .ToList();
                string errMsg = string.Join("|", errors);
                var result = ResultOutput.NotOk(errMsg);
                return new BadRequestObjectResult(result);
            };
        });
        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                types.ForEach(a =>
                {
                    var apiExplorerSettingsAttribute = a.GetCustomAttribute<ApiExplorerSettingsAttribute>();
                    if (apiExplorerSettingsAttribute != null && !string.IsNullOrEmpty(apiExplorerSettingsAttribute.GroupName))
                    {
                        options.SwaggerEndpoint($"/swagger/{apiExplorerSettingsAttribute.GroupName}/swagger.json", apiExplorerSettingsAttribute.GroupName);
                    }
                });
                // 将RoutePrefix设置为空字符串目的是http://localhost:xxx直接就导航到swagger页面【如果不加的话需要加上http://localhost:xxx/swagger】
                options.RoutePrefix = string.Empty;
            });
        }
        // 开启静态文件中间件
        app.UseStaticFiles();
        #region 开启Cors路由中间件
        // 注意: 要放到app.UseEndpoints前
        app.UseCors("Policy");
        #endregion

        // 启用静态文件中间件
        app.UseStaticFiles();
        // 认证
        app.UseAuthentication();
        // 授权
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}

