using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Extensions.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _log;
    public readonly IWebHostEnvironment _webHostEnviroment;
    public GlobalExceptionFilter(IWebHostEnvironment webHostEnviroment, ILogger<GlobalExceptionFilter> log)
    {
        _webHostEnviroment = webHostEnviroment;
        _log = log;
    }
    public void OnException(ExceptionContext context)
    {
        if (!context.ExceptionHandled)
        {
            var res = Failed("", (int)HttpStatusCode.InternalServerError, "服务器发生未处理的异常");

            if (_webHostEnviroment.IsDevelopment())
            {
                res.msgDev = res.msg + "：" + context.Exception.Message;
                res.response = context.Exception.StackTrace;
            }

            context.Result = new ContentResult
            {
                StatusCode = 500,
                ContentType = "application/json;charset=utf-8",
                Content = JsonConvert.SerializeObject(res),
            };

            _log.LogError($"服务器内部异常：{JsonConvert.SerializeObject(res)}");
            context.ExceptionHandled = true;
        }
    }
}
