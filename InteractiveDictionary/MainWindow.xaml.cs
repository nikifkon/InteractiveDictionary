using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using InteractiveDictionary.domain;
using System.Windows;
using System.IO;

namespace InteractiveDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :IWordRepository
    {
        public Word SelectedWord = new Word(11, "Test", "Тест", new List<Tag> {new Tag("tag1"), new Tag("tag2")}, "We don't need tests", DateTime.Now, "no comments");
        public List<Word> Words = new() { };
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = SelectedWord;
            Deck.ItemsSource = GetWords();

        }

        private void deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWord = (Word)Deck.SelectedItem;
            DataContext = SelectedWord; // ?!?!??!?!?!?
        }

        public List<Word> GetWords()
        {
            List<Word> wordList = new List<Word>();
            var lines = File.ReadAllLines(@"a.txt");
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
                var date = parts[5].Substring(parts[5].IndexOf(':')).Split(' ')[0].Split('.');
                var time = parts[5].Substring(parts[5].IndexOf(':')).Split(' ')[1].Split(':');
                var createAt = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                var comment = parts[6].Split(':')[1];
                wordList.Add(new Word(id, foreighForm, translated, tags, example, createAt, comment));
            }
            return wordList;
        }

        private void AddWord(object sender, RoutedEventArgs e)
        {
            var window = new WindowForAddWord();
            window.Show();
        }

        public List<Tag> GetUsedTags()
        {
            throw new NotImplementedException();
        }
    }
}