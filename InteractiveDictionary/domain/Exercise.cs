namespace InteractiveDictionary.domain
{
    public record Exercise(string Question, string Answer)
    {
        public string Question { get; } = Question;
        public string Answer { get; } = Answer;
    }
}