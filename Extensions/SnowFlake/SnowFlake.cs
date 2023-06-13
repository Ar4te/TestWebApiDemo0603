namespace Extensions.SnowFlake;

public class SnowFlake
{
    // 上次计算Id的时间戳
    private long _lastTimestamp = -1L;
    // WorkId 的位数
    private readonly static int _workIdBits = 4;
    // 数据中心 Id 的位数
    private readonly static int _dataCenterIdBits = 4;
    // 最大工作者Id 31
    private readonly long _maxWorkerId = -1L * (-1L << 4);
    // 最大数据中心Id 31
    private readonly long _maxDataCneterId = -1L * (-1L << 4);
    // 序列号的位数
    private readonly static int _sequenceBits = 20 - _workIdBits - _dataCenterIdBits - 1;
    // worker ID 在时间戳中的位移量
    private readonly int _workerIdShift = 8;
    // 数据中心 ID 在时间戳中的位移量
    private readonly int _dataCenterIdShift = 14;
    // 时间戳的位移量
    private readonly int _timestampLeftShift = 18;
    // 序号掩码 4095
    private readonly long _sequenceMask = -1L * (-1L << _sequenceBits);
    // worker ID
    private long _workerId;
    // 数据中心 ID
    private long _dataCenterId;
    // 序号
    private long _sequence = 0L;

    private readonly object _lock = new();

    public SnowFlake(long workerId, long dataCenterId)
    {
        if (workerId > _maxWorkerId || workerId < 0)
            throw new ArgumentOutOfRangeException(nameof(workerId));

        if (dataCenterId > _maxDataCneterId || dataCenterId < 0)
            throw new ArgumentOutOfRangeException(nameof(dataCenterId));

        _workerId = workerId;
        _dataCenterId = dataCenterId;
    }

    public long NextId()
    {
        lock (_lock)
        {
            var timestamp = GetTimestamp();
            if (timestamp < _lastTimestamp)
                throw new ArgumentException($"Invalid timestamp. The last timestamp is {_lastTimestamp} and the current timestamp is {timestamp}");

            if (_lastTimestamp == timestamp)
            {
                _sequence = (_sequence + 1) & _sequenceMask;
                if (_sequence == 0) timestamp = WaitForNextTick(_lastTimestamp);
            }
            else _sequence = 0;

            var id = ((timestamp - 1288834974657L) << _timestampLeftShift) | (_dataCenterId << _dataCenterIdShift) | (_workerId << _workerIdShift) | _sequence;
            return id;
        }
    }

    private static long WaitForNextTick(long lastTimestamp)
    {
        var timestamp = GetTimestamp();
        while (timestamp <= lastTimestamp)
        {
            timestamp = GetTimestamp();
        }
        return timestamp;
    }

    private static long GetTimestamp() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}
