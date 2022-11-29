namespace InteractiveDictionary.domain
{
    public interface IExerciseGenerator
    {
        Exercise Generate(Word word);
    }
}