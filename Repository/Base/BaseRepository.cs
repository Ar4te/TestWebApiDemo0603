using Extensions.ApiContext;
using Extensions.DataBase.DB;
using Repository.UnitOfWork;
using SqlSugar;
using System.Linq.Expressions;

namespace Repository.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
{
    private SqlSugarScope _db;
    private readonly IUnitOfWorkManage _uow;
    public BaseRepository(IUnitOfWorkManage uow)
    {
        _uow = uow;
        _db = _uow.GetDbClient();
    }

    #region Create

    public async Task<int> Add(TEntity entity)
    {
        return await _db.Insertable(entity).ExecuteCommandAsync();
    }

    public async Task<int> AddList(List<TEntity> entities)
    {
        return await _db.Insertable(entities).ExecuteCommandAsync();
    }

    #endregion


    #region Query
    public async Task<List<TEntity>> GetAllDatas()
    {
        return await _db.Queryable<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetDataById(object Id)
    {
        return await _db.Queryable<TEntity>().InSingleAsync(Id);
    }

    public async Task<List<TEntity>> GetDatas(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _db.Queryable<TEntity>().Where(whereExpression).ToListAsync();
    }

    public async Task<List<TEntity>> GetDatasBySql(string sqlStr, SugarParameter[] sugarParams = null)
    {
        return await _db.Ado.SqlQueryAsync<TEntity>(sqlStr, sugarParams);
    }




    #endregion

    #region Delete
    public async Task<bool> DelData(Expression<Func<TEntity, bool>> whereExpression)
    {
        var res = await _db.Deleteable(whereExpression).ExecuteCommandAsync();
        return res > 0;
    }

    public async Task<bool> DelDatasByPKey(object[] pKeys)
    {
        var res = await _db.Deleteable<TEntity>().In(pKeys).ExecuteCommandAsync();
        return res > 0;
    }



    #endregion
}