using Extensions.ApiContext;
using IService.Base;
using Models.Models;

namespace IService;

public interface IUserService : IBaseService<User>
{
    Task<MessageModel<bool>> CreateAsync(User user);
    Task<MessageModel<List<User>>> GetAllData();
    Task<MessageModel<string>> GetToken(string uName, string uPassword);
}
