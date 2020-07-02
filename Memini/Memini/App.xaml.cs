using Memini.Views;
using System;
using Xamarin.Forms;
using Memini.Data;
using System.IO;

namespace Memini
{
    public partial class App : Application
    {
        static MeminiDatabase database;

        public static MeminiDatabase Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new MeminiDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Memini.db3"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("PUET:");
                        Console.WriteLine(ex.ToString());
                    }
                }
                Console.WriteLine("TEST:");
                Console.WriteLine(database.ToString());
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
