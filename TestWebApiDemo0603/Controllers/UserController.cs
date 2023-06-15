using Extensions.ApiContext;
using Extensions.Helper;
using Extensions.Swagger;
using IService;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;

namespace TestWebApiDemo0603.Controllers;

[ApiController]
[ApiRoute(ApiVersionInfo.V1, true, "api")]
public class UserController : ControllerBase
{
    private readonly IUserService _user;
    private readonly ILogger<User> _log;
    public UserController(IUserService user, ILogger<User> log)
    {
        _user = user;
        _log = log;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<MessageModel<bool>> CreateUser([FromForm] UserVM uvm)
    {
        var user = new User(uvm);
        return await _user.CreateAsync(user);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<MessageModel<List<User>>> GetUsers()
    {
        return await _user.GetAllData();
    }

    [HttpGet]
    public async Task<MessageModel<string>> GetToken(string uName, string uPassword)
    {
        if (string.IsNullOrEmpty(uName) || string.IsNullOrWhiteSpace(uName))
            return MessageModel.Failed("用户名不可为空");
        if (string.IsNullOrEmpty(uPassword) || string.IsNullOrWhiteSpace(uPassword))
            return MessageModel.Failed("密码不可为空");
        return await _user.GetToken(uName, uPassword);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<MessageModel<string>> PostFile([FromForm] fileUploadReq fileReq)
    {
        string savePath = $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Millisecond}";
        var res = FtpHelper.FtpUpload(fileReq.file, savePath);
        return await Task.FromResult(res.Item3 ? MessageModel.Succeed("ok") : MessageModel.Failed(res.Item1));
    }
}

public class fileUploadReq
{
    public IFormFile file { get; set; }
}