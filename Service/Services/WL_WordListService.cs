using Extensions.ApiContext;
using IService.IServices;
using Models.Models;
using Models.ViewModels;
using Repository.Base;
using Repository.UnitOfWork;
using Service.Base;

namespace Service.Services;

public class WL_WordListService : BaseService<WL_WordList>, IWL_WordListService
{
    private readonly IBaseRepository<WL_WordList> _dal;
    private readonly IUnitOfWorkManage _uow;
    public WL_WordListService(IBaseRepository<WL_WordList> dal, IUnitOfWorkManage uow)
    {
        _dal = dal;
        _baseDal = dal;
        _uow = uow;
    }

    public async Task<MessageModel<bool>> WriteDownWord(List<WordListVM> reqVms)
    {
        var models = new List<WL_WordList>();
        var newWords = reqVms;
        var words = reqVms.Select(reqVm => reqVm.word).ToList();
        if (words?.Any() == true)
        {
            var olds = await _dal.GetDatas(r => words.Contains(r.wwl_Word));
            if (olds?.Any() == true) newWords = reqVms.SkipWhile(v => olds?.Any(o => o.wwl_Word == v.word) == true).ToList();
        }
        newWords.ForEach(word => models.Add(new WL_WordList(word)));
        if (newWords?.Any() == true)
        {
            var res = await _dal.AddList(models) > 0;
            return res ? MessageModel.Succeed(res, msg: "记录成功") : MessageModel.Failed(res, 500, "系统异常");
        }
        else return MessageModel.Failed(false, msg: "单词均已存在，请勿重复添加");
    }

    public async Task<MessageModel<bool>> WriteDownWord(WordListVM reqVm)
    {
        var old = await _dal.GetDatas(r => r.wwl_Word == reqVm.word);
        if (old?.Any() == true) return MessageModel.Failed(false, msg: "单词已存在，请勿重复添加");
        var res = await _dal.Add(new WL_WordList(reqVm)) > 0;
        return res ? MessageModel.Succeed(res, msg: "记录成功") : MessageModel.Failed(res, 500, "系统异常");
    }

    public async Task<MessageModel<List<WordListVM>>> GetWords(string word)
    {
        var words = string.IsNullOrEmpty(word) ? await _dal.GetAllDatas() : await _dal.GetDatas(r => r.wwl_Word == word);
        var wordVms = new List<WordListVM>();
        words.ForEach(word => wordVms.Add(new WordListVM(word)));
        return MessageModel.Succeed(wordVms);
    }

    public async Task<MessageModel<bool>> UpdateWord(WordListVM reqVm)
    {
        try
        {
            _uow.BeginTran();
            var delRes = await _dal.DelData(r => r.wwl_Word == reqVm.word);
            var addRes = await _dal.Add(new WL_WordList(reqVm));
            if (delRes && addRes > 0)
            {
                _uow.CommitTran();
                return MessageModel.Succeed(true);
            }
            else
            {
                _uow.RollBackTran();
                return MessageModel.Failed(false, 500, $"delRes={delRes}*****addRes={addRes}");
            }
        }
        catch (Exception ex)
        {
            _uow.RollBackTran();
            return MessageModel.Failed(false, 500, ex.Message);
        }
    }

    public async Task<MessageModel<List<WordListVM>>> GetRandomWord(int count)
    {
        var unfamilarWords = await _dal.GetDatas(word => (word.wwl_IsEngraved & word.wwl_IsMastered) == false);
        var unfamilarWordVms = new List<WordListVM>();
        unfamilarWords.ForEach(word => unfamilarWordVms.Add(new WordListVM(word)));

        var rand = new Random();
        int i = 0;
        int[] indexs = new int[count];
        while (i < count)
        {
            var index = rand.Next(0, unfamilarWordVms.Count);
            while (indexs.Any(j => j == index))
            {
                index = rand.Next(0, unfamilarWordVms.Count);
            }
            indexs[i] = index;
            i++;
        }
        var res = new List<WordListVM>();
        for (int j = 0; j < unfamilarWordVms.Count; j++)
        {
            if (indexs.Contains(j))
            {
                res.Add(unfamilarWordVms[j]);
            }
        }
        return MessageModel.Succeed(res);
    }
}
