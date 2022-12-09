using System;
using System.Collections.Generic;
using System.IO;

namespace InteractiveDictionary.domain
{
    public class TextBasedWordRepository : IWordRepository
    {
        public readonly string Filepath;

        public TextBasedWordRepository(string filepath)
        {
            Filepath = filepath;
            if (!File.Exists(Filepath))
            {
                using var file = File.Create(Filepath);
            }
        }

        public List<Word> GetWords()
        {
            var words = new List<Word>();
            var lines = File.ReadAllLines(Filepath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                var id = int.Parse(parts[0].Split(':')[1]);
                var foreighForm = parts[1].Split(':')[1];
                var translated = parts[2].Split(':')[1];
                var tags = new List<Tag>();
                var stringList = new List<string>(parts[3].Split(':')[1].Split(','));
                foreach (var item in stringList)
                    tags.Add(new Tag(item));
                var example = parts[4].Split(':')[1];
                var test = parts[5].Substring(parts[5].IndexOf(':') + 1);
                var test1 = parts[5].Substring(parts[5].IndexOf(':') + 1).Split(' ')[1];
                var date = parts[5].Substring(parts[5].IndexOf(':') + 1).Split(' ')[0].Split('.');
                var time = parts[5].Substring(parts[5].IndexOf(':') + 1).Split(' ')[1].Split(':');
                var createAt = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]),
                    int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                var comment = parts[6].Split(':')[1];
                words.Add(new Word(id, foreighForm, translated, tags, example, createAt, comment));
            }

            return words;
        }

        public List<Tag> GetUsedTags()
        {
            throw new System.NotImplementedException();
        }
        public void AddWord(Word word)
        {
            //foreach (var property in word.GetType().GetProperties())
            //{
            //    streamWriter.Write(word.Tos);
            //}
            File.AppendAllText(Filepath, WordToString(word) + "\n");
        }

        public void DeleteWord(int id)
        {
            throw new NotImplementedException();
        }

        private string WordToString(Word word)
        {
            var result = "";
            result += $"Id:{word.Id};";
            result += $"ForeignForm:{word.ForeignForm};";
            result += $"Translated:{word.Translated};";
            var tags = "";
            foreach (var tag in word.Tags)
                tags += tag.Name + ",";
            result += $"Tags: {tags.Substring(0, tags.Length - 1)}; ";
            result += $"Example:{word.Example};";
            result += $"CreateAt:{word.CreateAt};";
            result += $"Comment:{word.Comment}";
            return result;
        }
    }
}