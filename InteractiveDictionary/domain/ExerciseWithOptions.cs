using System.Collections.Generic;

namespace InteractiveDictionary.domain
{
    public record ExerciseWithOptions(string Question, string Answer, List<string> Options) : Exercise(Question, Answer)
    {
        public List<string> Options { get; } = Options;
    }
}