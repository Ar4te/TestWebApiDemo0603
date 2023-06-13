using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Collections.Concurrent;

namespace Repository.UnitOfWork;

public class UnitOfWorkManage : IUnitOfWorkManage
{
    private readonly ILogger<UnitOfWorkManage> _log;
    private readonly ISqlSugarClient _sqlSugarClient;
    private string dbId = "";
    private int _tranCount { get; set; }
    public int TranCount => _tranCount;
    public readonly ConcurrentStack<string> TranStack = new();
    public UnitOfWorkManage(ILogger<UnitOfWorkManage> log, ISqlSugarClient sqlSugarClient)
    {
        _log = log;
        _sqlSugarClient = sqlSugarClient;
        _tranCount = 0;
    }

    public void BeginTran()
    {
        lock (this)
        {
            _tranCount++;
            GetDbClient().BeginTran();
        }
    }

    public void CommitTran()
    {
        lock (this)
        {
            _tranCount--;
            if (_tranCount == 0)
            {
                try
                {
                    GetDbClient().CommitTran();
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message + "*****" + ex.StackTrace);
                    GetDbClient().RollbackTran();
                }
            }
        }
    }

    public UnitOfWork CreateUnitOfWork()
    {
        UnitOfWork uow = new();
        uow.logger = _log;
        uow.Db = _sqlSugarClient;
        uow.Tenant = (ITenant)_sqlSugarClient;
        uow.IsTran = true;

        uow.Db.Open();
        uow.Tenant.BeginTran();
        _log.LogDebug("UnitOfWork Begin");
        return uow;
    }

    public IQueryable<T> FromSql<T>(string sql, params object[] parameters) where T : class, new()
    {
        dbId = "";
        return GetDbClient().SqlQueryable<T>(sql).ToList().AsQueryable();
    }

    public IQueryable<T> FromSqlFromDbId<T>(string sql, string dbId, params object[] parameters) where T : class, new()
    {
        this.dbId = dbId;
        return GetDbClient().SqlQueryable<T>(sql).ToList().AsQueryable();
    }

    public SqlSugarScope GetDbClient()
    {
        return _sqlSugarClient as SqlSugarScope;
    }

    public void RollBackTran()
    {
        lock (this)
        {
            _tranCount--;
            GetDbClient().RollbackTran();
        }
    }
}
