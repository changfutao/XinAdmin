using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xin.Infrastructure.Dto;
using Xin.Infrastructure.Extensions;

namespace Xin.Infrastructure.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(IWebHostEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
           if(context.ExceptionHandled == false)
            {
                string message = string.Empty;
                if(_env.IsProduction())
                {
                    message = Enums.StatusCodes.Status500InternalServerError.ToDescription();
                }
                else
                {
                    message = context.Exception.Message;
                }
                _logger.LogError(context.Exception, "");
                var data = ResultOutput.NotOk(message);
                context.Result = new InternalServerErrorResult(data);
            }
            context.ExceptionHandled = true;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }

    public class InternalServerErrorResult : ObjectResult
    {
        public InternalServerErrorResult(object value) : base(value)
        {
            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
        }
    }
}
