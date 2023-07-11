using Extensions.ApiContext;
using Extensions.Swagger;
using IService.IServices;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace TestWebApiDemo0603.Controllers;

[ApiController]
[ApiRoute(ApiVersionInfo.V1, true, "api")]

public class PrefixDataController : ControllerBase
{
    private readonly IPrefixDataServices _pd;
    public PrefixDataController(IPrefixDataServices pd)
    {
        _pd = pd;
    }
    [HttpPost]
    public async Task<MessageModel<List<PrefixDataVM>>> GetDataFromExcel([FromForm] fileUploadReq formData)
    {
        return await _pd.GetDataFromExcel(formData.file);
    }

}
