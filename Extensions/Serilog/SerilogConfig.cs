namespace Extensions.Serilog;

public class SerilogConfig
{
    public static string serilogTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss:fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";

    private static string basePath = Environment.CurrentDirectory;

    public static string debugPath() => basePath + @"\Log\Debug\.log";
    public static string infoPath() => basePath + @"\Log\Info\.log";
    public static string warnPath() => basePath + @"\Log\Warn\.log";
    public static string errorPath() => basePath + @"\Log\Error\.log";
    public static string fatalPath() => basePath + @"\Log\Fatal\.log";
}
