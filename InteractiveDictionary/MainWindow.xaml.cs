using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using InteractiveDictionary.domain;
using System.Windows;
using System.IO;
using System.Linq;

namespace InteractiveDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Word SelectedWord;
        public ObservableCollection<Word> Words;
        public IWordRepository Repository;

        public MainWindow()
        {
            InitializeComponent();
            Repository = new TextBasedWordRepository(@"a.txt");
            Words = new ObservableCollection<Word>(Repository.GetWords());
            Deck.ItemsSource = Words;
            SelectedWord = Words.FirstOrDefault();
            if (SelectedWord is not null)
            {
                var tags = "";
                foreach (var tag in SelectedWord.Tags)
                    tags += tag.Name + ", ";
                TagsList.Text = tags;
                DataContext = SelectedWord;
            }
        }

        private void deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWord = (Word) Deck.SelectedItem;
            DataContext = SelectedWord; // ?!?!??!?!?!?
            var tags = "";
            foreach (var tag in SelectedWord.Tags)
                tags += tag.Name + ", ";
            TagsList.Text = tags;
        }

        private void AddWord(object sender, RoutedEventArgs e)
        {
            var window = new WindowForAddWord(Repository, word => Words.Add(word));
            window.Show();
        }

        private void StartStudySession(object sender, RoutedEventArgs e)
        {
            var dialog = new StudySessionWindow(Repository);
            dialog.ShowDialog();
        }
    }
}