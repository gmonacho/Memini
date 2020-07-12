using Memini.Models;
using System;
using Xamarin.Forms;

namespace Memini.Views
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                listView.ItemsSource = await App.Database.GetThemesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        async void OnThemeAddedClicked(object sender, EventArgs e)
        {
            string name = await DisplayPromptAsync("Theme Name :",
                                                     "Please, enter the new theme's name");
            if (name != null)
            {
                if (name.Length > 0)
                {
                    await Navigation.PushAsync(new ThemePage
                    {
                        BindingContext = new Theme
                        {
                            Name = name
                        }
                    });
                }
                else
                    OnThemeAddedClicked(sender, e);
            }
        }

        async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new ThemePage
                {
                    BindingContext = e.Item as Theme
                });
            }
        }
    }
}