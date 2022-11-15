using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using InteractiveDictionary.domain;

namespace InteractiveDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Word SelectedWord = new Word(11, "Test", "Тест", new List<Tag> {new Tag("tag1"), new Tag("tag2")}, "We don't need tests", DateTime.Now, "no comments");
        public ObservableCollection<Word> Words = new()
        {
            new Word(11, "Test", "Тест", new List<Tag> {new("tag1"), new("tag2")}, "We don't need tests", DateTime.Now, "no comments"),
            new Word(12, "Test2", "Тест2", new List<Tag> {new("tag1"), new("tag2")}, "We don't need tests", DateTime.Now, "no comments"),
            new Word(13, "Test3", "Тест3", new List<Tag> {new("tag1"), new("tag2")}, "We don't need tests", DateTime.Now, "no comments"),
            new Word(14, "Test4", "Тест4", new List<Tag> {new("tag1"), new("tag2")}, "We don't need tests", DateTime.Now, "no comments"),
            new Word(15, "Test5", "Тест5", new List<Tag> {new("tag1"), new("tag2")}, "We don't need tests", DateTime.Now, "no comments"),
            new Word(16, "Test6", "Тест6", new List<Tag> {new("tag1"), new("tag2")}, "We don't need tests", DateTime.Now, "no comments"),
        };
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = SelectedWord;
            Deck.ItemsSource = Words;
        }

        private void deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWord = (Word)Deck.SelectedItem;
            DataContext = SelectedWord; // ?!?!??!?!?!?
        }
    }
}