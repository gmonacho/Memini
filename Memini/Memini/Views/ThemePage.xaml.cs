using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Memini.Models;

namespace Memini.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemePage : ContentPage
    {
        private string _newWord;
        private string _newTranslation;
        private bool _toolbarHide;

        public ThemePage()
        {
            InitializeComponent();
            _newWord = string.Empty;
            _newTranslation = string.Empty;
            _toolbarHide = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var theme = (Theme)BindingContext;

            listWord.ItemsSource = theme.Words;
            if (theme.ID == 0)
            {
                theme.Name = "New Theme";
                await App.Database.SaveThemeAsync(theme);
            }
        }

        private void HideEditGrid()
        {
            Resources["editWordStyle"] = Resources["editWordHideStyle"];
            editWordGrid.RowDefinitions[0].Height = editWordGrid.HeightRequest;
            modifyToolbar.Text = "Add";
            _toolbarHide = true;
        }
        
        private void ShowEditGrid()
        {
            Resources["editWordStyle"] = Resources["editWordShowStyle"];
            editWordGrid.RowDefinitions[0].Height = editWordGrid.HeightRequest;
            modifyToolbar.Text = "Ok";
            _toolbarHide = false;
        }

        private void ToggleEditGrid()
        {
            if (_toolbarHide == true)
                ShowEditGrid();
            else
                HideEditGrid();
        }

        private void EmptyEditSystem()
        {
            _newWord = string.Empty;
            _newTranslation = string.Empty;
            wordEntry.Text = _newWord;
            translationEntry.Text = _newTranslation;
        }

        async private void AddNewWord(string v1, string v2)
        {
            var theme = (Theme)BindingContext;
            var word = new Word
            {
                V1 = v1,
                V2 = v2
            };
            await App.Database.SaveWordAsync(word);
            theme.Words.Add(word);
            await App.Database.SaveThemeAsync(theme);
        }
        
        async private void EditExistingWord(string v1, string v2, int id)
        {
            Theme theme = (Theme)BindingContext;

            foreach (Word word in theme.Words)
            {
                if (word.ID == id)
                {
                    await App.Database.DeleteWordAsync(word);
                    theme.Words.Remove(word);
                    AddNewWord(v1, v2);
                    break;
                }
            }
        }

        void OnWordAddToolbarClicked(object sender, EventArgs e)
        {
            if (_toolbarHide == false)
            {
                if (listWord.SelectedItem == null)
                {
                    if (_newWord.Length > 0 && _newTranslation.Length > 0)
                        AddNewWord(_newWord, _newTranslation);
                }
                else
                {
                    Word word = (Word)listWord.SelectedItem;
                    EditExistingWord(_newWord, _newTranslation, word.ID);
                    listWord.SelectedItem = null;
                }
            }
            ToggleEditGrid();
            deleteToolbar.Text = "Delete";
            EmptyEditSystem();
        }

        async void OnDeleteToolbarClicked(object sender, EventArgs e)
        {
            if (listWord.SelectedItem == null)
            {
                await App.Database.DeleteThemeAsync((Theme)BindingContext);
                await Navigation.PopAsync();
            }
            else
            {
                Theme theme = (Theme)BindingContext;
                await App.Database.DeleteWordAsync((Word)listWord.SelectedItem);
                theme.Words.Remove((Word)listWord.SelectedItem);
                listWord.SelectedItem = null;
                HideEditGrid();
                EmptyEditSystem();
            }

        }

        void OnWordTextChanged(object sender, TextChangedEventArgs e)
        {
            _newWord = e.NewTextValue;
        }

        void OnTranslationTextChanged(object sender, TextChangedEventArgs e)
        {
            _newTranslation = e.NewTextValue;
        }
        
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Word word = (Word)e.Item;
            wordEntry.Text = word.V1;
            translationEntry.Text = word.V2;
            ShowEditGrid();
            deleteToolbar.Text = "Delete Word";
        }
    }
}
