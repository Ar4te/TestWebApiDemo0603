using Extensions.Helper;

namespace Extensions.DataBase.DB;

public class BaseDBConfig
{
    public static string Connection = AppSettings.app("DBS", "ConnectionString");
}

public enum DbType
{
    MySql = 1,
    SqlServer,
    Sqlite,
    Oracle,
    PostgreSQL,
    Dm,
    Kdbndp
}
