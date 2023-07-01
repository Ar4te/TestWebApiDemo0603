using Extensions.ApiContext;
using IService.IServices;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.ViewModels;
using Repository.Base;
using Service.Base;

namespace Service.Services;

public class SpotCheckService : BaseService<SpotCheck>, ISpotCheckService
{
    private readonly ILogger<SpotCheckService> _logger;
    private readonly IBaseRepository<SpotCheck> _dal;
    //private readonly I
    public SpotCheckService(IBaseRepository<SpotCheck> dal, ILogger<SpotCheckService> logger)
    {
        _logger = logger;
        _baseDal = dal;
        _dal = dal;
    }

    public async Task GetSCPlan()
    {
        var datas = await _dal.GetAllDatas();
        SpotCheck.TestMethod(datas);
    }

    public async Task<MessageModel<string>> MarkDownSCPlan(SpotCheckVM scvm)
    {
        var res = await _dal.Add(new SpotCheck(scvm));
        return res > 0 ? MessageModel.Succeed("记录成功") : MessageModel.Failed("记录失败");
    }

    public async Task<MessageModel<string>> GenereateSCPlan()
    {
        var datas = await _dal.GetAllDatas();

        // 找出日点检
        var _datas = (from SpotCheck sc in datas
                      group sc by sc.CheckType into scs
                      select scs).ToList();

        _datas.ToList().ForEach(_data =>
        {

        });
        return null;
    }
}
