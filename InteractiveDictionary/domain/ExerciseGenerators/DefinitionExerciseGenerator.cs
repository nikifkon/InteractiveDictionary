using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace InteractiveDictionary.domain
{
    
    public class DefinitionExerciseGenerator : IExerciseGenerator
    {
        public const string ApiEndpoint = "https://api.dictionaryapi.dev/api/v2/entries/en/";
        
        public Exercise Generate(Word word)
        {
            var request = WebRequest.Create(ApiEndpoint + word.ForeignForm);
            request.Method = "GET";

            try
            {
                using var webResponse = request.GetResponse();
                using var webStream = webResponse.GetResponseStream();
                using var reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();
                var firstDefinition = new Regex(@",""definitions"":\[{""definition"":""(.*?)"",""");
                var definition = firstDefinition.Match(data).Groups[1].Value;
                return new Exercise(definition, word.ForeignForm);
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}