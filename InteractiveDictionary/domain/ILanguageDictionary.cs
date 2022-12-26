using System.Collections.Generic;

namespace InteractiveDictionary.domain
{
    public interface ILanguageDictionary
    {
        public TranslationResult TranslateToRussian(string word);
        public List<string> GetDefinitionInForeign(string word);
        public List<string> ListSynonyms(string word);
    }
}