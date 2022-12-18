using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using InteractiveDictionary.domain;
using System.Windows;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace InteractiveDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Word? SelectedWord;
        public ListSortDirection listSortDirection = ListSortDirection.Ascending;
        public ObservableCollection<Word> Words;
        public ApplicationContext db = new ApplicationContext();
        public ICollectionView WordsView { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            db.Database.EnsureCreated();
            db.Words.Load();
            db.Tags.Load();

            Words = db.Words.Local.ToObservableCollection();
            WordsView = CollectionViewSource.GetDefaultView(Words);
            WordsView.Filter += o => Filter(o as Word);
            WordsView.SortDescriptions.Add(new SortDescription("ForeignForm", listSortDirection));
            Deck.ItemsSource = WordsView;
        }


        private void deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedWord = (Word) Deck.SelectedItem;
            DataContext = SelectedWord;
        }

        private void AddWord(object sender, RoutedEventArgs e)
        {
            var window = new AddWordForm(db);
            window.ShowDialog();
        }

        private void DeleteWord(object sender, RoutedEventArgs e)
        {
            if (SelectedWord is null) return;
            db.Words.Remove(SelectedWord);
            db.SaveChanges();
            Words.Remove(SelectedWord);
        }

        private void StartStudySession(object sender, RoutedEventArgs e)
        {
            var words = WordsView.Cast<Word>().ToList();
            if (words.Count == 0)
            {
                MessageBox.Show("No words to study");
                return;
            }
            var dialog = new StudySessionWindow(words);
            dialog.ShowDialog();
        }

        private bool Filter(Word word)
        {
            return SearchTextBox.Text == "" || word.Translated.Contains(SearchTextBox.Text) ||
                   word.ForeignForm.Contains(SearchTextBox.Text);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            WordsView.Refresh();
        }

        private void SearchTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                WordsView.Refresh();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox) sender).Text == "")
            {
                WordsView.Refresh();
            }
        }

        private void SortWords(object sender, RoutedEventArgs e)
        {
            listSortDirection = listSortDirection == ListSortDirection.Ascending
                ? ListSortDirection.Descending
                : ListSortDirection.Ascending;
            
            WordsView.SortDescriptions.Clear();
            WordsView.SortDescriptions.Add(new SortDescription("ForeignForm", listSortDirection));
        }
    }
}