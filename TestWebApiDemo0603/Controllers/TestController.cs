using Extensions.Swagger;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace TestWebApiDemo0603.Controllers;

[ApiController]
[ApiRoute(ApiVersionInfo.V2, true, "test")]
public class TestController : ControllerBase
{
    public TestController()
    {

    }

    [HttpGet]
    public IActionResult EncryptBase64(string input)
    {
        return Ok(Convert.ToBase64String(Encoding.UTF8.GetBytes(input)));
    }

    [HttpGet]
    public IActionResult DecryptBase64(string input)
    {
        return Ok(Encoding.UTF8.GetString(Convert.FromBase64String(input)));
    }
}
