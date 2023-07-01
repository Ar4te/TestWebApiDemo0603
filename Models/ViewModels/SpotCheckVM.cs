namespace Models.ViewModels;

public class SpotCheckVM
{
    public SpotCheckVM()
    {

    }
    public string PlanNo { get; set; }
    public string CheckType { get; set; }
    public List<CheckItem> CheckItems { get; set; }
    public List<PlanTime> PlanTime { get; set; }
    public int CreaterId { get; set; }
    public int ModifierId { get; set; }
}

public class CheckItem
{
    public string Item { get; set; }
    public string Value { get; set; }
    public string Unit { get; set; }
}

//public class PlanTime
//{
//    public DateTime? DaliyTime { get; set; }
//    public DateTime? WeeklyTime { get; set; }
//    public DateTime? SemimonthlyTime1 { get; set; }
//    public DateTime? SemimonthlyTime2 { get; set; }
//    public DateTime? MonthlyTime { get; set; }
//    public DateTime? SemiannualTime1 { get; set; }
//    public DateTime? SemiannualTime2 { get; set; }
//    public DateTime? YearlyTime { get; set; }
//}

public class PlanTime
{
    public string Flag { get; set; }
    public DateTime DetailTime { get; set; }
}
