using System.Collections.Generic;

namespace InteractiveDictionary.domain
{
    public interface IWordRepository
    {
        List<Word> GetWords();
        List<Tag> GetUsedTags();
        void AddWord(Word word);
    }
}