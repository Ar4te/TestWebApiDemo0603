using Extensions.SnowFlake;
using Models.ViewModels;
using SqlSugar;

namespace Models.Models;

[SugarTable("WL_Sentence")]
public class WL_Sentence
{
    public WL_Sentence()
    {

    }

    public WL_Sentence(SentenceVM reqVm, long wwl_Id)
    {
        wls_Sentence = reqVm.sentence;
        wls_WordId = wwl_Id.ToString();
    }

    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long wls_Id { get; set; }

    [SugarColumn(IsNullable = false, ColumnDataType = "longtext")]
    public string wls_Sentence { get; set; }

    [SugarColumn(IsNullable = false)]
    public string wls_WordId { get; set; }
}
