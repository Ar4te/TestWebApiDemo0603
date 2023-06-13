using Models.Models;
using Repository.Base;
using Repository.UnitOfWork;

namespace Repository;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(IUnitOfWorkManage uow) : base(uow)
    {

    }
}
