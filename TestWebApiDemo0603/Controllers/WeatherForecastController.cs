using Extensions.ApiContext;
using Extensions.Swagger;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models;

namespace TestWebApiDemo0603.Controllers;


[ApiController]
[ApiRoute(ApiVersionInfo.V1, true, "api")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMapper _map;
    private readonly IMemoryCache _mCache;
    public WeatherForecastController(IMapper map, IMemoryCache mCache)
    {
        _map = map;
        _mCache = mCache;
    }

    /// <summary>
    /// ≤‚ ‘1
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get1()
    {
        var model = _map.Map<Test>(new TestDto("12", "Arete"));
        var res = _mCache.Set("Test", model);
        return Ok(model);
    }

    /// <summary>
    /// ≤‚ ‘2
    /// </summary>
    /// <param name="cacheName"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get2(string cacheName)
    {
        return Ok(_mCache.Get(cacheName));
    }

    /// <summary>
    /// ≤‚ ‘3
    /// </summary>
    /// <param name="ss"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<MessageModel<string>> MakeAnException(string? ss = null)
    {
        if (ss != null) return await Task.Run(() => MessageModel.Succeed(ss));
        throw new Exception("Test GlobalExceptionFilter");
    }
}