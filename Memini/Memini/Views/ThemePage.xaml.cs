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
        private object _selectedComponent;

        public ThemePage()
        {
            InitializeComponent();
            _toolbarHide = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var theme = (Theme)BindingContext;

            listWord.ItemsSource = theme.Words;
            if (theme.ID == 0)
            {
                Console.WriteLine("ICI");
                theme.Name = "New Theme";
                await App.Database.SaveThemeAsync(theme);
            }
        }

        private void HideEditGrid()
        {
            newWordGrid.HeightRequest = 0;
            newWordGrid.RowDefinitions[0].Height = 0;
            modifyToolbar.Text = "Modify";
            _toolbarHide = true;
        }

        private void ShowEditGrid()
        {
            newWordGrid.HeightRequest = 60;
            newWordGrid.RowDefinitions[0].Height = 60;
            modifyToolbar.Text = "Add";
            _toolbarHide = false;
        }

        private void ToggleEditGrid()
        {
            if (_toolbarHide == true)
                ShowEditGrid();
            else
                HideEditGrid();
        }

        void OnWordAddToolbarClicked(object sender, EventArgs e)
        {
            ToggleEditGrid();
        }

        async void OnThemeDeleteToolbarClicked(object sender, EventArgs e)
        {
            await App.Database.DeleteThemeAsync((Theme)BindingContext);
            await Navigation.PopAsync();
        }

        void OnWordTextChanged(object sender, TextChangedEventArgs e)
        {
            _newWord = e.NewTextValue;
        }

        void OnTranslationTextChanged(object sender, TextChangedEventArgs e)
        {
            _newTranslation = e.NewTextValue;
        }

        async void OnWordAddedClicked(object sender, EventArgs e)
        {
            var theme = (Theme)BindingContext;
            var word = new Word
            {
                V1 = _newWord,
                V2 = _newTranslation
            };

            await App.Database.SaveWordAsync(word);
            theme.Words.Add(word);
            await App.Database.SaveThemeAsync(theme);

            ToggleEditGrid();
        }
        
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Word word = (Word)e.Item;
            _selectedComponent = word;
            wordEntry.Text = word.V1;
            translationEntry.Text = word.V2;
            ShowEditGrid();
        }
    }
}
