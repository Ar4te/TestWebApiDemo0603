namespace Models.ViewModels;

public class PrefixDataVM
{
    public PrefixDataVM()
    {

    }
    public PrefixDataVM(string sn, string dataStr)
    {
        Sn = sn;
        DataStr = dataStr;
    }
    public string Sn { get; set; }
    public string DataStr { get; set; }
}
