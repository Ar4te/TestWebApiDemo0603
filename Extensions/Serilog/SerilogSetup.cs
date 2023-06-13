using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Extensions.Serilog;

public static class SerilogSetup
{
    public static IHostBuilder AddSerilogSetUp(this IHostBuilder host)
    {
        host.UseSerilog((ctx, log) =>
        {
            log.Enrich.FromLogContext();
            log.WriteTo.Console();
            log.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.Async(a => a.File(SerilogConfig.debugPath(), rollingInterval: RollingInterval.Day, outputTemplate: SerilogConfig.serilogTemplate)))
            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.Async(a => a.File(SerilogConfig.infoPath(), rollingInterval: RollingInterval.Day, outputTemplate: SerilogConfig.serilogTemplate)))
            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File(SerilogConfig.warnPath(), rollingInterval: RollingInterval.Day, outputTemplate: SerilogConfig.serilogTemplate)))
            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.Async(a => a.File(SerilogConfig.errorPath(), rollingInterval: RollingInterval.Day, outputTemplate: SerilogConfig.serilogTemplate)))
            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.Async(a => a.File(SerilogConfig.fatalPath(), rollingInterval: RollingInterval.Day, outputTemplate: SerilogConfig.serilogTemplate)))
            ;
        });
        return host;
    }
}
