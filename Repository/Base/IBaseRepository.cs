using Extensions.ApiContext;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;

namespace Repository.Base;

public interface IBaseRepository<TEntity> where TEntity : class
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
}