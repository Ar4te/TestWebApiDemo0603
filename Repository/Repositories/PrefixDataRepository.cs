using Models.Models;
using Repository.Base;
using Repository.UnitOfWork;

namespace Repository.Repositories;

public class PrefixDataRepository : BaseRepository<PrefixData>
{
    public PrefixDataRepository(IUnitOfWorkManage uow) : base(uow)
    {
    }
}
