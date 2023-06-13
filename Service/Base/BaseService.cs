using IService.Base;
using Repository.Base;

namespace Service.Base;

public class BaseService<T> : IBaseService<T> where T : class, new()
{
    public IBaseRepository<T> _baseDal { get; set; }
    public BaseService(IBaseRepository<T> baseDal)
    {
        _baseDal = baseDal;
    }
    public Task<T> GetAllDatas()
    {
        throw new NotImplementedException();
    }
}