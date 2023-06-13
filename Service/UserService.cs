using Extensions.ApiContext;
using Extensions.Helper;
using Extensions.JWT;
using IService;
using Models.Models;
using Repository.Base;
using Service.Base;

namespace Service;

public class UserService : BaseService<User>, IUserService
{
    private readonly IBaseRepository<User> _dal;
    private readonly TokenHelper _tokenHelper;
    public UserService(IBaseRepository<User> dal, TokenHelper tokenHelper) : base(dal)
    {
        _dal = dal;
        _baseDal = dal;
        _tokenHelper = tokenHelper;
    }

    public async Task<MessageModel<bool>> CreateAsync(User user)
    {
        var res = await _dal.Add(user);
        return res > 0 ? MessageModel.Succeed(res > 0) : MessageModel.Failed<bool>();
    }

    public async Task<MessageModel<List<User>>> GetAllData()
    {
        var datas = await _dal.GetAllDatas();
        return datas != null && datas.Any() ? MessageModel.Succeed(datas) : MessageModel.Failed<List<User>>(msg: "系统异常");
    }

    public async Task<MessageModel<string>> GetToken(string uName, string uPassword)
    {
        var _encryptStr = MD5Helper.MD5Encrypt32(uPassword);
        var user = await _dal.GetDatas(u => u.UserName == uName && u.Password == _encryptStr);
        if (user == null || user.Count <= 0) return MessageModel.Failed("用户名或密码错误");

        var token = _tokenHelper.CreateJwtToken(user[0]);
        return MessageModel.Succeed(token);
    }
}
