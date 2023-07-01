using Extensions.ApiContext;
using Extensions.Swagger;
using IService.IServices;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace TestWebApiDemo0603.Controllers;

[ApiController]
[ApiRoute(ApiVersionInfo.V1, true, "api")]
public class SpotCheckController : ControllerBase
{
    private readonly ISpotCheckService _scService;
    public SpotCheckController(ISpotCheckService scService)
    {
        _scService = scService;
    }

    [HttpPost]
    public async Task<MessageModel<string>> MarkDownPlan([FromBody] SpotCheckVM scvm)
    {
        return await _scService.MarkDownSCPlan(scvm);
    }

    [HttpGet]
    public async Task GetPlan()
    {
        await _scService.GetSCPlan();
    }
}
