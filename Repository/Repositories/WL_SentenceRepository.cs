using Models.Models;
using Repository.Base;
using Repository.UnitOfWork;

namespace Repository.Repositories;

public class WL_SentenceRepository : BaseRepository<WL_Sentence>
{
    public WL_SentenceRepository(IUnitOfWorkManage uow) : base(uow)
    {
    }
}
