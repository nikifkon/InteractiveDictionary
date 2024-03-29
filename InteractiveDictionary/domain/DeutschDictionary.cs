﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;


namespace InteractiveDictionary.domain
{
    public class DeutschDictionary : ILanguageDictionary
    {
        private Regex _definitionSectionRegex =
            new(@"Bedeutungen:</b><br/>((.|\s)*?)</p>");

        private Regex _definitionRegex = new(@"(?<=</span>)(?<tag>(?:.|\s)*?)(?:$|<)");

        private Regex _translationRegex = new(@"(?<=<tr .*?>)<text>(?<translation>.*?)</text>");

        private Regex _synonymRegex =
            new Regex(@"Synonyme:</b><br/>\s*?<span class='wiktionaryItem'> 1\.</span>(.*)");

        public DeutschDictionary()
        {
        }

        public TranslationResult TranslateToRussian(string word)
        {
            var url = "https://dictionary.yandex.net/api/v1/dicservice/lookup?";
            // free key. You can get your own key here: https://tech.yandex.com/dictionary/ or use mine
            var yandexKey = "dict.1.1.20221209T124847Z.00bf4f5b08f53dc6.b6eb82405f9b69ccfedd80afb1b8de86aa106be9";
            url += $"key={yandexKey}";
            url += "&lang=de-ru";
            url += $"&text={word}";
            var request = WebRequest.Create(url);
            request.Method = "Get";
            WebResponse webResponse;
            Stream webStream;
            try
            {
                webResponse = request.GetResponse();
            }
            catch
            {

                return new TranslationResult("",ErrorType.NetworkError, "Ошибка подключения к сети, введите свой перевод");
            }
            try
            {
                webStream = webResponse.GetResponseStream();
                using var reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();
                //_translationRegex.Match(data).Groups["translation"].Value
                string text = _translationRegex.Match(data).Groups["translation"].Value;
                if (text.Length > 0)
                    return new TranslationResult(text);
                else
                    return new TranslationResult("", ErrorType.TranslationNotFound, $"Перевод {word} не найден, введите свой");
            }
            catch
            {
                return new TranslationResult("", ErrorType.TranslationNotFound, $"Перевод {word} не найден, введите свой");
            }
        }

        public List<string> GetDefinitionInForeign(string word)
        {
            var url = $"https://www.openthesaurus.de/synonyme/{word}";
            var request = WebRequest.Create(url);
            request.Method = "GET";
            WebResponse webResponse;
            Stream webStream;
            try
            {
                webResponse = request.GetResponse();
                webStream = webResponse.GetResponseStream();
                using var reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();
                var defSection = _definitionSectionRegex.Match(data).Groups[1].Value;
                var matches = _definitionRegex.Matches(defSection);
                return matches
                    .Cast<Match>()
                    .Select(m => m.Groups["tag"])
                    .Select(capture => capture.Value.Trim())
                    .Where(value => value != "")
                    .ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public List<string> ListSynonyms(string word)
        {
            throw new System.NotImplementedException();
        }
    }
}