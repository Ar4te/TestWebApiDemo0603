using Models.Models;
using Repository.Base;
using Repository.UnitOfWork;

namespace Repository.Repositories;

public class SpotCheckRecordRepository : BaseRepository<SpotCheckRecord>
{
    public SpotCheckRecordRepository(IUnitOfWorkManage uow) : base(uow)
    {
    }
}
