using System;
using System.Collections.Generic;
using System.IO;

namespace InteractiveDictionary.domain
{
    public class Word
    {
        public Word(int id, string foreignForm, string translated, List<Tag> tags, string example, DateTime createAt, string comment)
        {
            Id = id;
            ForeignForm = foreignForm;
            Translated = translated;
            Tags = tags;
            Example = example;
            CreateAt = createAt;
            Comment = comment;
        }

        public int Id { get; }
        public string ForeignForm { get; }
        public string Translated { get; }
        public List<Tag> Tags { get; }
        public string Example { get; }
        public DateTime CreateAt { get; }
        public string Comment { get; }

        public void AddWord()
        {
            //foreach (var property in word.GetType().GetProperties())
            //{
            //    streamWriter.Write(word.Tos);
            //}
            File.AppendAllText(@"a.txt", this.ToString()+"\n");
        }

        public override string ToString()
        {
            var result = "";
            result += string.Format("Id:{0};", Id);
            result += string.Format("ForeignForm:{0};", ForeignForm);
            result += string.Format("Translated:{0};", Translated);
            var tags = "";
            foreach (var tag in Tags)
                tags += tag.Name + ",";
            result += string.Format("Tags:{0};", tags.Substring(0,tags.Length-1));
            result += string.Format("Example:{0};", Example);
            result += string.Format("CreateAt:{0};", CreateAt);
            result += string.Format("Comment:{0}", Comment);
            return result;
        }
    }
}