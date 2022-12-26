using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveDictionary.domain
{
    public enum ErrorType
    {
        None,
        NetworkError,
        TranslationNotFound
    }

    public class TranslationResult
    {
        public string TranslatedText;
        public ErrorType ErrorType;
        public string ErrorMessage;

        public TranslationResult( string translatedText, ErrorType errorType, string errorMessage)
        {
            TranslatedText = translatedText;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }

        public TranslationResult(string translatedText)
        {
            TranslatedText = translatedText;
            ErrorType = ErrorType.None;
            ErrorMessage = "";
        }
    }
}
