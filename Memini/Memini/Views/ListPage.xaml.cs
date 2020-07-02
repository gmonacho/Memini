using Memini.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            await Navigation.PushAsync(new ThemePage
            {
                BindingContext = new Theme()
            });
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