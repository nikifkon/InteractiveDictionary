using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using InteractiveDictionary.domain;
using System.IO;

namespace InteractiveDictionary
{
    /// <summary>
    /// Логика взаимодействия для WindowForAddWord.xaml
    /// </summary>
    public partial class WindowForAddWord : Window
    {
        public Word NewWord;
        public IWordRepository Repository;
        public Action<Word> InsertWordCallback;
        
        public WindowForAddWord(IWordRepository repository, Action<Word> insertWordCallback)
        {
            InitializeComponent();
            Repository = repository;
            InsertWordCallback = insertWordCallback;
        }

        private void deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void AddWord(object sender, RoutedEventArgs e)
        {
            var count = 0;
            if (File.Exists(@"a.txt"))
                count = File.ReadAllLines(@"a.txt").Length;
            var id = count + 1;
            var foreighForm = TextBoxForForeignForm.Text;
            var translated = TextBoxForTranslated.Text;
            var tags = new List<Tag>();
            var stringList = new List<string>(TextBoxForTags.Text.Split(' '));
            foreach (var item in stringList)
                tags.Add(new Tag(item));
            var example = TextBoxForExample.Text;
            var createAt = DateTime.Now;
            var comment = TextBoxForComment.Text;
            NewWord = new Word(id, foreighForm, translated, tags, example, createAt, comment);
            Repository.AddWord(NewWord);
            InsertWordCallback(NewWord);
            MessageBox.Show("Действие выполнено");
            this.Close();
        }
    }
}
