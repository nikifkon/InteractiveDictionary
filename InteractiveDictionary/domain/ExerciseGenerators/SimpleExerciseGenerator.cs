namespace InteractiveDictionary.domain
{
    public class SimpleExerciseGenerator : IExerciseGenerator
    {
        public Exercise Generate(Word word)
        {
            return new Exercise(word.ForeignForm, word.Translated);
        }
    }
}