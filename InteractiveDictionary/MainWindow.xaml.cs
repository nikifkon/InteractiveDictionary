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
    public partial class MainWindow 
    {
        public Word SelectedWord;
        public ObservableCollection<Word> Words;

        public MainWindow()
        {
            InitializeComponent();
            Words = new ObservableCollection<Word>(GetWords());
            Deck.ItemsSource = Words;
            SelectedWord = Words[0];
            var tags = "";
            foreach (var tag in SelectedWord.Tags)
                tags += tag.Name + ", ";
            TagsList.Text = tags;
            DataContext = SelectedWord;
        }

        private void deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWord = (Word)Deck.SelectedItem;
            DataContext = SelectedWord; // ?!?!??!?!?!?
            var tags = "";
            foreach (var tag in SelectedWord.Tags)
                tags += tag.Name + ", ";
            TagsList.Text = tags;
        }

        public List<Word> GetWords()
        {
            var words = new List<Word>();
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
                var test = parts[5].Substring(parts[5].IndexOf(':') + 1);
                var test1 = parts[5].Substring(parts[5].IndexOf(':') + 1).Split(' ')[1];
                var date = parts[5].Substring(parts[5].IndexOf(':') + 1).Split(' ')[0].Split('.');
                var time = parts[5].Substring(parts[5].IndexOf(':') + 1).Split(' ')[1].Split(':');
                var createAt = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                var comment = parts[6].Split(':')[1];
                words.Add(new Word(id, foreighForm, translated, tags, example, createAt, comment));
            }
            return words;
        }

        private void AddWord(object sender, RoutedEventArgs e)
        {
            var window = new WindowForAddWord(this);
            window.Show();
        }

        public List<Tag> GetUsedTags()
        {
            throw new NotImplementedException();
        }
    }
}