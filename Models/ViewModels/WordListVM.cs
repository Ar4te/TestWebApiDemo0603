using Models.Models;

namespace Models.ViewModels;

public class WordListVM
{
    public WordListVM()
    {

    }
    public WordListVM(WL_WordList _word)
    {
        word = _word.wwl_Word;
        cInterpretation = _word.wwl_DescByChinese;
        isEngraved = _word.wwl_IsEngraved;
        isMastered = _word.wwl_IsMastered;
    }

    public string word { get; set; }
    public string? cInterpretation { get; set; }
    public bool isEngraved { get; set; } = false;
    public bool isMastered { get; set; } = false;
}

public class wlVms
{
    public List<WordListVM> vms { get; set; }
}


public class WordListVM2
{
    public List<string> Words { get; set; }
    public string Sentence { get; set; }
}