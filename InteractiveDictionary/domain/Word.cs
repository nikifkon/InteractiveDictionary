using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;


namespace InteractiveDictionary.domain
{
    public class Word
    {
        public int Id { get; set; }
        public string ForeignForm { get; set; }
        public string Translated { get; set; }
        public string? Definition { get; set; }
        public List<Tag> Tags { get; set; }
        private static Random Random = new Random();

        public Word()
        {
            Tags = new List<Tag>();
        }

        public Exercise GetRandomExercise()
        {
            if (Definition != null && Random.NextDouble() > 0.4)
            {
                return new Exercise(Definition, ForeignForm, "Какое слово соответствует определению?");
            }

            if (Random.NextDouble() > 0.5)
            {
                return new Exercise(ForeignForm, Translated, "Как переводится слово?");
            }

            return new Exercise(Translated, ForeignForm, "Как на немецком будет?");
        }
    }
}