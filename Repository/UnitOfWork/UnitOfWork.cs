using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Repository.UnitOfWork;

public class UnitOfWork
{
    public ILogger logger { get; set; }
    public ISqlSugarClient Db { get; set; }
    public ITenant Tenant { get; set; }
    public bool IsTran { get; set; }
    public bool IsCommit { get; set; }
    public bool IsClose { get; set; }
    public void Dispose()
    {
        if (IsTran && !IsCommit)
        {
            logger.LogDebug("UnitOfWork RollBackTran");
            Tenant.RollbackTran();
        }

        if (Db.Ado.Transaction != null || IsClose)
            return;
        Db.Close();
    }

    public bool Commit()
    {
        if (IsTran && !IsCommit)
        {
            logger.LogDebug($"UnitOfWOrk CommitTran");
            Tenant.CommitTran();
            IsCommit = true;
        }

        if (Db.Ado.Transaction == null && IsClose)
        {
            Db.Close();
            IsClose = true;
        }
        return IsCommit;

    }
}
