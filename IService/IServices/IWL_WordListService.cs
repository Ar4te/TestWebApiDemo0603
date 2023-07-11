using Extensions.ApiContext;
using IService.Base;
using Models.Models;
using Models.ViewModels;

namespace IService.IServices;

public interface IWL_WordListService : IBaseService<WL_WordList>
{
    Task<MessageModel<bool>> WriteDownWord(List<WordListVM> reqVms);
    Task<MessageModel<bool>> WriteDownWord(WordListVM reqVm);
    Task<MessageModel<List<WordListVM>>> GetWords(string word);
    Task<MessageModel<bool>> UpdateWord(WordListVM reqVm);
    Task<MessageModel<List<WordListVM>>> GetRandomWord(int count);
}
