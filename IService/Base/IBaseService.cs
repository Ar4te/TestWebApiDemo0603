using SqlSugar;
using System.Linq.Expressions;

namespace IService.Base;

public interface IBaseService<TEntity> where TEntity : class
{
    #region Create
    Task<int> Add(TEntity entity);
    Task<int> AddList(List<TEntity> entities);
    #endregion
    #region Query
    Task<List<TEntity>> GetAllDatas();
    Task<List<TEntity>> GetDatas(Expression<Func<TEntity, bool>> whereExpression);
    Task<List<TEntity>> GetDatasBySql(string sqlStr, SugarParameter[] sugarParams = null);
    Task<TEntity> GetDataById(object Id);
    #endregion
    #region Delete
    Task<bool> DelData(Expression<Func<TEntity, bool>> whereExpression);
    Task<bool> DelDatasByPKey(object[] pKeys);
    #endregion
}