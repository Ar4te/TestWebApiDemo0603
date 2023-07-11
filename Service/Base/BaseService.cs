using IService.Base;
using Repository.Base;
using SqlSugar;
using System.Linq.Expressions;
namespace Service.Base;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
{
    public IBaseRepository<TEntity> _baseDal { get; set; }
    public BaseService()
    {

    }
    public BaseService(IBaseRepository<TEntity> baseDal)
    {
        _baseDal = baseDal;
    }

    #region Create

    public async Task<int> Add(TEntity entity)
    {
        return await _baseDal.Add(entity);
    }

    public async Task<int> AddList(List<TEntity> entities)
    {
        return await _baseDal.AddList(entities);
    }

    #endregion


    #region Query
    public async Task<List<TEntity>> GetAllDatas()
    {
        return await _baseDal.GetAllDatas();
    }

    public async Task<TEntity> GetDataById(object Id)
    {
        return await _baseDal.GetDataById(Id);
    }

    public async Task<List<TEntity>> GetDatas(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _baseDal.GetDatas(whereExpression);
    }

    public async Task<List<TEntity>> GetDatasBySql(string sqlStr, SugarParameter[] sugarParams = null)
    {
        return await _baseDal.GetDatasBySql(sqlStr, sugarParams);
    }




    #endregion

    #region Delete
    public async Task<bool> DelData(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _baseDal.DelData(whereExpression);
    }

    public async Task<bool> DelDatasByPKey(object[] pKeys)
    {
        return await _baseDal.DelDatasByPKey(pKeys);
    }



    #endregion
}