using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

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
    }
}
