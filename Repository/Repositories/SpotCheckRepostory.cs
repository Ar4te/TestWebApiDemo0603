using Models.Models;
using Repository.Base;
using Repository.UnitOfWork;

namespace Repository.Repositories;

public class SpotCheckRepostory : BaseRepository<SpotCheck>
{
    public SpotCheckRepostory(IUnitOfWorkManage uow) : base(uow)
    {
    }
}
