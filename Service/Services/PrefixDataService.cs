using Extensions.ApiContext;
using Extensions.SnowFlake;
using IService.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.ViewModels;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
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

    public async Task<MessageModel<List<PrefixDataVM>>> GetDataFromExcel(IFormFile file)
    {
        try
        {
            var wb = new XSSFWorkbook(file.OpenReadStream());
            var sheetCount = wb.Count;
            string errMsg = string.Empty;
            List<PrefixData> datas = new();
            for (int i = 0; i < sheetCount; i++)
            {
                var dealRes = DealSheetData(wb.GetSheetAt(i));
                if (dealRes.Item3) datas.AddRange(dealRes.Item1);
                else errMsg = string.IsNullOrEmpty(errMsg) ? dealRes.Item2 : errMsg + ";\r\n" + dealRes.Item2;
            }

            if (datas.Count <= 0) return MessageModel.Failed<List<PrefixDataVM>>(msg: errMsg);
            _uow.BeginTran();
            try
            {
                var res = await _dal.AddList(datas);
                if (res > 0)
                {
                    _uow.CommitTran();
                    return MessageModel.Succeed<List<PrefixDataVM>>(response: null, msg: "");
                }
                else
                {
                    _uow.RollBackTran();
                    return MessageModel.Failed<List<PrefixDataVM>>(msg: $"{res}\r\n{errMsg}");
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + "*****" + ex.StackTrace);
                _uow.RollBackTran();
                return MessageModel.Failed<List<PrefixDataVM>>(msg: ex.Message + "*****" + ex.StackTrace + "\r\n" + errMsg);
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex.Message + "*****" + ex.StackTrace);
            return MessageModel.Failed<List<PrefixDataVM>>(msg: ex.Message + "*****" + ex.StackTrace);
        }
    }


    public static Tuple<List<PrefixData>, string, bool> DealSheetData(ISheet sheet)
    {
        var sheetName = sheet.SheetName;
        var rows = sheet.LastRowNum; if (rows <= 0) return new(null, $"工作表{sheetName}无内容", false);
        var sets = sheet.GetRow(0).Cells.ToArray();
        if (sets == null || sets.Length <= 0) return new(null, $"工作表{sheetName}无表头", false);

        string[] snSets = new string[] { "SN", "条码", "BARCODE", "电芯编码" };
        int snIndex = -1;
        var snSet = sets?.Where(set => snSets.Contains(set.ToString()?.ToUpper()));
        if (snSet?.Count() == 1) snIndex = Array.IndexOf(sets, snSet.First());
        else
        {
            if (snIndex < 0) return new(null, $"工作表{sheetName}表头未注明条码列", false);
            else return new(null, $"工作表{sheetName}检测到多个条码列，请注意区分", false);
        }

        List<PrefixData> pd = new();
        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            Console.WriteLine(i);
            var cells = sheet.GetRow(i).Cells;
            var sn = cells[snIndex].ToString();
            Dictionary<string, string> dataDic = new();
            for (int j = 0; j < cells.Count; j++)
            {
                if (j == snIndex) continue;
                var key = sets[j]?.ToString();
                var val = cells[j]?.ToString();
                if (dataDic.ContainsKey(key)) dataDic[key] = val;
                else dataDic.Add(key, val);
            }

            var dataStr = JsonConvert.SerializeObject(dataDic);
            pd.Add(new PrefixData(YittHelper.NextId(), sn, dataStr));
        }

        return new(pd, "", true);
    }
}
