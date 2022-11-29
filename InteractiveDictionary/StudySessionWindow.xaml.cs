using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using InteractiveDictionary.domain;

namespace InteractiveDictionary
{
    public partial class StudySessionWindow : Window
    {
        public IReadOnlyList<Exercise> Exercises
        {
            get => (IReadOnlyList<Exercise>) GetValue(ExercisesProperty);
            set => SetValue(ExercisesProperty, value);
        }

        public static readonly DependencyProperty ExercisesProperty =
            DependencyProperty.Register("Exercises", typeof(IReadOnlyList<Exercise>), typeof(StudySessionWindow));

        public Exercise CurrentExercise
        {
            get => (Exercise) GetValue(CurrentExerciseProperty);
            set => SetValue(CurrentExerciseProperty, value);
        }

        public static readonly DependencyProperty CurrentExerciseProperty =
            DependencyProperty.Register("CurrentExercise", typeof(Exercise), typeof(StudySessionWindow));

        public int CurrentExerciseNumber
        {
            get => (int) GetValue(CurrentExerciseNumberProperty);
            set => SetValue(CurrentExerciseNumberProperty, value);
        }

        public static readonly DependencyProperty CurrentExerciseNumberProperty =
            DependencyProperty.Register("CurrentExerciseNumber", typeof(int), typeof(StudySessionWindow));

        public bool IsShowingAnswer
        {
            get => (bool) GetValue(IsShowingAnswerProperty);
            set => SetValue(IsShowingAnswerProperty, value);
        }

        public static readonly DependencyProperty IsShowingAnswerProperty =
            DependencyProperty.Register("IsShowingAnswer", typeof(bool), typeof(StudySessionWindow));

        public StudySessionWindow(IWordRepository repository)
        {
            Dispatcher.BeginInvoke(new Action<IWordRepository>(GetExercise), repository);
            InitializeComponent();
        }

        private void GetExercise(IWordRepository repository)
        {
            var generators = new List<IExerciseGenerator>()
            {
                new SimpleExerciseGenerator(),
                new DefinitionExerciseGenerator()
            };
            var random = new Random();
            Exercises = repository.GetWords()
                .Select(word => generators[random.Next() % generators.Count].Generate(word))
                .Where(exercise => exercise is not null)
                .ToList();
            CurrentExerciseNumber = 1;
            CurrentExercise = Exercises[CurrentExerciseNumber - 1];
            IsShowingAnswer = false;
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            if (!IsShowingAnswer)
            {
                IsShowingAnswer = true;
                return;
            }

            if (++CurrentExerciseNumber > Exercises.Count)
            {
                Close();
                return;
            }

            IsShowingAnswer = false;
            CurrentExercise = Exercises[CurrentExerciseNumber - 1];
        }
    }
}