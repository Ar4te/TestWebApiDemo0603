using Extensions.ApiContext;
using IService.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.ViewModels;
using Newtonsoft.Json;
using NPOI.XSSF.UserModel;
using Repository.Base;
using Repository.UnitOfWork;
using Service.Base;

namespace Service.Services;

public class PrefixDataService : BaseService<PrefixData>, IPrefixDataServices
{
    private readonly IBaseRepository<PrefixData> _dal;
    private readonly IUnitOfWorkManage _uow;
    private readonly ILogger<PrefixData> _log;
    public PrefixDataService(IBaseRepository<PrefixData> baseDal, IUnitOfWorkManage uow, ILogger<PrefixData> log)
    {
        _dal = baseDal;
        _baseDal = baseDal;
        _uow = uow;
        _log = log;
    }

    public async Task<MessageModel<List<PrefixData>>> GetDataFromExcel(IFormFile file)
    {
        var wb = new XSSFWorkbook(file.OpenReadStream());
        var worksheet = wb.GetSheetAt(0);
        var sets = worksheet.GetRow(0).Cells.ToArray();
        if (sets == null || sets.Length <= 0) return MessageModel.Failed<List<PrefixData>>(msg: "选择了空文件");

        string[] snSets = new string[] { "SN", "sn", "Sn", "sN", "条码" };
        int snIndex = -1;
        var snSet = sets?.Where(set => snSets.Contains(set.ToString()));
        if (snSet?.Count() > 0) snIndex = Array.IndexOf(sets, snSet.First());

        if (snIndex < 0) return MessageModel.Failed<List<PrefixData>>(msg: "选择的文件无条码列");

        List<PrefixDataVM> pdVM = new();
        for (int i = 1; i <= worksheet.LastRowNum; i++)
        {
            var cells = worksheet.GetRow(i).Cells;
            var sn = cells[snIndex].ToString();
            Dictionary<string, List<string>> dataDic = new();
            for (int j = 0; j < cells.Count; j++)
            {
                if (j == snIndex) continue;
                var key = sets[j]?.ToString();
                var val = cells[j]?.ToString();
                if (dataDic.ContainsKey(key)) dataDic[key].Add(val);
                else dataDic.Add(key, new List<string> { val });
            }

            var dataStr = JsonConvert.SerializeObject(dataDic);
            pdVM.Add(new PrefixDataVM(sn, dataStr));
        }
        var pd = new List<PrefixData>();
        pdVM?.ForEach(p => pd.Add(new PrefixData(p)));
        _uow.BeginTran();
        try
        {
            var res = await _dal.AddList(pd);
            if (res > 0)
            {
                _uow.CommitTran();
                return MessageModel.Succeed(response: pd, msg: "");
            }
            else
            {
                _uow.RollBackTran();
                return MessageModel.Failed<List<PrefixData>>(msg: $"{res}");
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex.Message);
            _uow.RollBackTran();
            return MessageModel.Failed<List<PrefixData>>(msg: ex.Message);
        }
    }
}
