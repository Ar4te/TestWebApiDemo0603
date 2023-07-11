namespace Extensions.SnowFlake;

public class YittHelper
{
    public static SnowFlake _snowFlake { get; set; }
    public YittHelper()
    {
        var _workerId = Convert.ToInt64("1");
        var _dataCenterId = Convert.ToInt64("1");
        _snowFlake = new SnowFlake(_workerId, _dataCenterId);
    }

    public static long NextId() => _snowFlake.NextId();
}
