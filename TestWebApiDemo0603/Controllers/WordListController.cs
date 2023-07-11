using Extensions.ApiContext;
using Extensions.Swagger;
using IService.IServices;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace TestWebApiDemo0603.Controllers;


[ApiController]
[ApiRoute(ApiVersionInfo.V2, true, "wordList")]
public class WordListController : ControllerBase
{
    private readonly IWL_WordListService _wl;
    public WordListController(IWL_WordListService wl)
    {
        _wl = wl;
    }

    [HttpPost]
    public async Task<MessageModel<bool>> WriteDownWord([FromForm] wlVms reqVms)
    {
        if (reqVms == null || !reqVms.vms?.Any() == true) return MessageModel.Failed(false, 500, "参数不可为空");
        return await _wl.WriteDownWord(reqVms?.vms);
    }

    [HttpPost]
    public async Task<MessageModel<bool>> WriteDownWord2([FromForm] WordListVM reqVm)
    {
        if (reqVm == null) return MessageModel.Failed<bool>(msg: "参数不可为空");
        return await _wl.WriteDownWord(reqVm);
    }

    [HttpGet]
    public async Task<MessageModel<List<WordListVM>>> GetWords(string? word)
    {
        word ??= "";
        return await _wl.GetWords(word);
    }

    [HttpPost]
    public async Task<MessageModel<bool>> UpdateWord([FromForm] WordListVM reqVm)
    {
        if (reqVm == null) return MessageModel.Failed<bool>(msg: "参数不可为空");
        return await _wl.UpdateWord(reqVm);
    }

    [HttpGet]
    public async Task<MessageModel<List<WordListVM>>> GetRandomWord(int count = 1)
    {
        return await _wl.GetRandomWord(count);
    }
}
