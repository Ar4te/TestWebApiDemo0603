using Extensions.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SqlSugar;

namespace Extensions.DataBase.SqlSugarSetUp;

public static class SqlSugarSetup
{
    public static void AddSqlSugarSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton<ISqlSugarClient>(options =>
        {
            // 数据库连接基础配置
            var connCfg = new ConnectionConfig
            {
                // 连接ID
                ConfigId = AppSettings.app("DBS", "ConfigId"),
                // 连接字符串
                ConnectionString = AppSettings.app("DBS", "ConnectionString"),
                // 数据库类型
                DbType = DbType.MySql,
                // 是否自动关闭连接
                IsAutoCloseConnection = true,

                AopEvents = new AopEvents
                {
                    OnLogExecuting = (sql, p) =>
                    {
                        if (AppSettings.app("SqlAOP", "Enabled").ToUpper() == "TRUE")
                        {
                            Parallel.For(0, 1, e =>
                            {
                                Log.Information("SqlLog", new string[] { sql.GetType().ToString(), GetParams(p), "【SQL语句】：" + sql });
                            });
                        }

                        if (AppSettings.app("SqlAOP", "LogToConsole").ToUpper() == "TRUE")
                        {
                            Console.WriteLine(string.Join("\r\n", new string[] { "--------", $"{DateTime.Now:yyyyMMdd:HHmmss} : " + GetWholeSql(p, sql) }), ConsoleColor.DarkCyan);
                        }
                    }
                },
                MoreSettings = new ConnMoreSettings
                {
                    // 自动移除数据缓存
                    IsAutoRemoveDataCache = true,
                },
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (property, column) =>
                    {
                        if (column.IsPrimarykey && column.IsIdentity && property.PropertyType == typeof(int))
                        {
                            column.IsIdentity = true;
                        }
                    }
                },
                // 从特性读取主键和自增键信息
                InitKeyType = InitKeyType.Attribute
            };
            return new SqlSugarScope(connCfg);
        });
    }

    public static string GetWholeSql(SugarParameter[] pars, string sql)
    {
        foreach (var param in pars)
        {
            sql.Replace(param.ParameterName, param.Value?.ToString());
        }
        return sql;
    }
    public static string GetParams(SugarParameter[] sqlParams)
    {
        string key = "【SQL参数】：";
        foreach (var param in sqlParams)
        {
            key += $"{param.ParameterName}:{param.Value}\n";
        };
        return key;
    }
}
