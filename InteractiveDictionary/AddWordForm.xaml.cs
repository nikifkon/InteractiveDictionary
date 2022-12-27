using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InteractiveDictionary.domain;

namespace InteractiveDictionary
{
    public partial class AddWordForm : Window
    {
        private int c = 0;
        private readonly ILanguageDictionary _dictionary;
        private readonly ObservableCollection<string> _definitionList = new();
        private readonly ApplicationContext db;
        private string? SelectedDefinision;

        public AddWordForm(ApplicationContext db)
        {
            InitializeComponent();
            this.db = db;
            _dictionary = new DeutschDictionary();
            DefinitionsStackPanel.ItemsSource = _definitionList;

            var thread = new Thread(StartForeignWordTextBoxWatcher);
            thread.Start();
        }

        public void IndicateError(TranslationResult translationResult)
        {
            var errorType = translationResult.ErrorType;
            switch (errorType) {
                case ErrorType.NetworkError:
                    ErrorBlock.Text = translationResult.ErrorMessage;
                    break;
                case ErrorType.TranslationNotFound:
                    ErrorBlock.Text = translationResult.ErrorMessage;
                    break;
                case ErrorType.None:
                    ErrorBlock.Text = "";
                    break;
            }
        }

        private void StartForeignWordTextBoxWatcher()
        {
            int prevHandled = c;
            while (true)
            {
                int prevC;
                do
                {
                    prevC = c;
                    Thread.Sleep(500);
                } while (!(prevC == c && prevHandled != c));

                var result = Dispatcher.BeginInvoke(new Func<string>(() => ForeignWordTextBox.Text));
                result.Wait();
                var foreignWord = result.Result.ToString();
                var definitions = _dictionary.GetDefinitionInForeign(foreignWord);
                var result2 = Dispatcher.BeginInvoke(new Action(() =>
                {
                    _definitionList.Clear();
                    foreach (var (definition, index) in definitions.Select((value, index) => (value, index)))
                    {
                        _definitionList.Add(definition);
                    }

                }));
                var translation = _dictionary.TranslateToRussian(foreignWord);
                var result3 = Dispatcher.BeginInvoke(new Action(() =>
                {
                    IndicateError(translation);
                    TranslatedWordTextBox.Text = translation.TranslatedText;
                }));
                
                prevHandled = c;
            }
        }

        private void ForeignWordTextBox_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            c++;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var word = new Word();
            if (ForeignWordTextBox.Text == "")
            {
                MessageBox.Show("Введите слово");
                return;
            }
            word.ForeignForm = ForeignWordTextBox.Text;
            
            if (TranslatedWordTextBox.Text == "")
            {
                MessageBox.Show("Введите перевод");
                return;
            }
            word.Translated = TranslatedWordTextBox.Text;

            word.Definition = SelectedDefinision;
            db.Words.Add(word);
            db.SaveChanges();
            Close();
        }
        
        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedDefinision = ((TextBlock) ((RadioButton) sender).Content).Text;
        }
    }
}