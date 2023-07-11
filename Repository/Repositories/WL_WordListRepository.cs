using Models.Models;
using Repository.Base;
using Repository.UnitOfWork;

namespace Repository.Repositories;

public class WL_WordListRepository : BaseRepository<WL_WordList>
{
    public WL_WordListRepository(IUnitOfWorkManage uow) : base(uow)
    {
    }
}
