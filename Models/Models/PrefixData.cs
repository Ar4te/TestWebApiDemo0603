using Extensions.SnowFlake;
using Models.ViewModels;
using SqlSugar;

namespace Models.Models;

[SugarTable("PrefixData")]
public class PrefixData
{
    public PrefixData()
    {
    }

    public PrefixData(long id, string sn, string dataStr)
    {
        pd_Id = id;
        pd_Sn = sn;
        pd_DataStr = dataStr;
        pd_CreateTime = DateTime.Now;
    }

    [SugarColumn(IsPrimaryKey = true)]
    public long pd_Id { get; set; }
    public string pd_Sn { get; set; }
    [SugarColumn(IsNullable = true)]
    public string pd_DataStr { get; set; }
    public DateTime pd_CreateTime { get; set; }
}
