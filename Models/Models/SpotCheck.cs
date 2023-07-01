using Dm;
using Extensions.SnowFlake;
using Models.ViewModels;
using Newtonsoft.Json;
using SqlSugar;

namespace Models.Models;

[SugarTable("SpotCheck")]
public class SpotCheck
{
    public SpotCheck()
    {

    }
    public SpotCheck(SpotCheckVM scvm)
    {
        Id = YittHelper.NextId();
        No = scvm.PlanNo;
        CheckItems = JsonConvert.SerializeObject(scvm.CheckItems);
        PlanTime = JsonConvert.SerializeObject(scvm.PlanTime);
        CheckType = scvm.CheckType;
        CreateTime = DateTime.Now;
        CreaterId = scvm.CreaterId;
        ModifierId = scvm.ModifierId;
    }
    [SugarColumn(IsPrimaryKey = true)]
    public long Id { get; set; }

    public string No { get; set; }

    [SugarColumn(IsNullable = true)]
    public string CheckItems { get; set; }

    public string CheckType { get; set; }

    public string PlanTime { get; set; }

    public DateTime CreateTime { get; set; }

    public int CreaterId { get; set; }

    [SugarColumn(IsNullable = true)]
    public DateTime? ModifyTime { get; set; }

    [SugarColumn(IsNullable = true)]
    public int ModifierId { get; set; }

    public static void TestMethod(List<SpotCheck> scs)
    {
        var needChecks = new List<SpotCheck>();
        scs.ForEach(sc =>
        {
            var pts = JsonConvert.DeserializeObject<List<PlanTime>>(sc.PlanTime);
            if (pts?.All(pt => pt.DetailTime == null) == true) throw new ArgumentException("未配置点检时间");
            else
            {
                var _dTime1 = (DateTime)pts[0]?.DetailTime;
                var _dTime2 = (DateTime)pts[1]?.DetailTime;
                var _now = DateTime.Now;
                // 先看详细时间，时间到了再看点检模式
                switch (sc.CheckType)
                {
                    case "1": if (_dTime1.AddDays(1) >= _now) needChecks.Add(sc); break;
                    case "2": if (_dTime1.AddDays(7) >= _now) needChecks.Add(sc); break;
                    case "3": if (_dTime1 >= _now || _dTime2 >= _now) needChecks.Add(sc); break;
                    default:
                        break;
                }
                //if (IsTimeToCheck(_now, _dTime1))
                //{
                //    switch (sc.CheckType)
                //    {
                //        case "1": needChecks.Add(sc); break;
                //        case "2": if (_now.DayOfWeek.ToString() == pts[0].Flag) needChecks.Add(sc); break;
                //        case "3":
                //            if (_now.)
                //            {

                //            }
                //            break;
                //        default:
                //            break;
                //    }
                //}
                //else if (IsTimeToCheck(_now, _dTime2))
                //{

                //}
                //else { }
            }
        });
    }

    static bool IsTimeToCheck(DateTime time1, DateTime time2, bool onlyCheckTime = false)
    {
        var res = false;
        if (onlyCheckTime) res = time1.Hour >= time2.Hour & time1.Minute >= time2.Minute & time1.Second >= time2.Second;
        else res = time1.Year >= time2.Year & time1.Month >= time2.Month & time1.Day >= time2.Day & time1.Hour >= time2.Hour & time1.Minute >= time2.Minute & time1.Second >= time2.Second;
        return res;
    }
}
