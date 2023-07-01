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

    public PrefixData(PrefixDataVM pdvm)
    {
        pd_Id = YittHelper.NextId();
        pd_Sn = pdvm.Sn;
        pd_DataStr = pdvm.DataStr;
    }
    [SugarColumn(IsPrimaryKey = true)]
    public long pd_Id { get; set; }
    public string pd_Sn { get; set; }
    [SugarColumn(IsNullable = true)]
    public string pd_DataStr { get; set; }
}
