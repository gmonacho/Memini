using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Memini.Models;
using Android.Content.Res;
using Xamarin.Essentials;

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
                await App.Database.SaveThemeAsync(theme);
            }
        }

        private void ShowAddGrid()
        {
            Resources["editWordStyle"] = Resources["editWordShowStyle"];
            editWordGrid.RowDefinitions[0].Height = editWordGrid.HeightRequest;
            modifyToolbar.Text = "Ok";
            editWordGrid.ColumnDefinitions[0].Width = Application.Current.MainPage.Width;
            editWordGrid.ColumnDefinitions[1].Width = 0;
            _toolbarHide = false;
        }

        private void HideEditGrid()
        {
            Resources["editWordStyle"] = Resources["editWordHideStyle"];
            editWordGrid.RowDefinitions[0].Height = editWordGrid.HeightRequest;
            modifyToolbar.Text = "Add";
            _toolbarHide = true;
            deleteToolbar.Text = "Delete";
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
                ShowAddGrid();
            else
                HideEditGrid();
        }

        private void EmptyEditSystem()
        {
            _newWord = string.Empty;
            _newTranslation = string.Empty;
            wordEntry.Text = _newWord;
            japaneseEntry.Text = _newTranslation;
        }

        async private void AddNewWord(Word word)
        {
            if (word != null)
            {
                var theme = (Theme)BindingContext;

                await App.Database.SaveWordAsync(word);
                theme.Words.Add(word);
                await App.Database.SaveThemeAsync(theme);
            }
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
                //    AddNewWord(v1, v2);
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
                    if (_newWord.Length > 0)
                    {
                        AddNewWord(App.Dict.GetWord(_newWord));
                    }
                }
                else
                {
                    Word word = (Word)listWord.SelectedItem;
                    EditExistingWord(_newWord, _newTranslation, word.ID);
                    listWord.SelectedItem = null;
                }
            }
            ToggleEditGrid();
            EmptyEditSystem();
        }

        async void OnDeleteToolbarClicked(object sender, EventArgs e)
        {
            Theme theme = (Theme)BindingContext;

            if (listWord.SelectedItem == null)
            {
                if (await DisplayAlert("Delete Theme",
                                        $"Would you like to delete {theme.Name}",
                                        "Yes", "No"))
                {
                    await App.Database.DeleteThemeAsync((Theme)BindingContext);
                    await Navigation.PopAsync();
                }
            }
            else
            {
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
            wordEntry.Text = word.Translation;
            japaneseEntry.Text = word.Kanji;
            ShowEditGrid();
            deleteToolbar.Text = "Delete Word";
        }
    }
}
