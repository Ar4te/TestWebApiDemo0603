using Extensions.SnowFlake;
using Models.ViewModels;
using SqlSugar;

namespace Models.Models;

[SugarTable("WL_WordList")]
public class WL_WordList
{
    public WL_WordList()
    {

    }

    public WL_WordList(WordListVM reqVm)
    {
        wwl_Word = reqVm.word;
        wwl_DescByChinese = reqVm.cInterpretation;
        wwl_IsEngraved = reqVm.isEngraved;
        wwl_IsMastered = reqVm.isMastered;
    }

    public WL_WordList(string word)
    {
        wwl_Word = word;
        wwl_DescByChinese = "";
        wwl_IsEngraved = false;
        wwl_IsMastered = false;
    }

    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int wwl_Id { get; set; }

    public string wwl_Word { get; set; }

    [SugarColumn(ColumnDataType = "longtext", IsNullable = true)]
    public string wwl_DescByChinese { get; set; }

    [SugarColumn(DefaultValue = "b'0'")]
    public bool wwl_IsEngraved { get; set; }

    [SugarColumn(DefaultValue = "b'0'")]
    public bool wwl_IsMastered { get; set; }
}
