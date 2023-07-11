using Extensions.ApiContext;
using IService.IServices;
using Models.Models;
using Models.ViewModels;
using Repository.Base;
using Repository.UnitOfWork;
using Service.Base;

namespace Service.Services;

public class WL_SentenceService : BaseService<WL_Sentence>, IWL_SentenceService
{
    private readonly IBaseRepository<WL_Sentence> _dal;
    private readonly IWL_WordListService _wlw;
    private readonly IUnitOfWorkManage _uow;
    public WL_SentenceService(IBaseRepository<WL_Sentence> dal, IWL_WordListService wlw, IUnitOfWorkManage uow)
    {
        _dal = dal;
        _baseDal = dal;
        _wlw = wlw;
        _uow = uow;
    }

    public async Task<MessageModel<bool>> MakeSentence(SentenceVM reqVm)
    {
        var words = await _wlw.GetDatas(w => w.wwl_Word == reqVm.word);
        if (words?.Any() == false)
        {
            var wordModel = new WL_WordList(reqVm.word);
            try
            {
                _uow.BeginTran();
                var wAdd = (await _wlw.Add(wordModel)) > 0;
                var sAdd = (await _dal.Add(new WL_Sentence(reqVm, wordModel.wwl_Id))) > 0;
                if (wAdd && sAdd)
                {
                    _uow.CommitTran();
                    return MessageModel.Succeed<bool>();
                }
                else
                {
                    _uow.RollBackTran();
                    return MessageModel.Failed(false, 500, $"wAdd={wAdd}*****sAdd={sAdd}");
                }
            }
            catch (Exception ex)
            {
                _uow.RollBackTran();
                return MessageModel.Failed(false, 500, ex.Message + "*****" + ex.StackTrace);
            }
        }
        else
        {
            var sAdd = (await _dal.Add(new WL_Sentence(reqVm, words.First().wwl_Id))) > 0;
            return sAdd ? MessageModel.Succeed<bool>() : MessageModel.Failed(false, 500, $"sAdd={sAdd}");
        }
    }
}
