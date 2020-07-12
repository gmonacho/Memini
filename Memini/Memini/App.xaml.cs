using Memini.Views;
using System;
using Xamarin.Forms;
using Memini.Data;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Memini.Models;
using System.Xml.Linq;

namespace Memini
{
    public partial class App : Application
    {
        static MeminiDatabase   database;
        static XmlDict          dict;

        public static MeminiDatabase Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new MeminiDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Memini.db3"));
                        return database;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("PUET:");
                        Console.WriteLine(ex.ToString());
                    }
                }
                return database;
            }
        }

        public static XmlDict Dict
        {
            get
            {
                return dict;
            }
        }


        public App()
        {
            InitializeComponent();
            dict = null;
            MainPage = new NavigationPage(new ListPage());
        }

        public App(StreamReader xmlStream)
        {
            InitializeComponent();
            try
            {
                dict = new XmlDict(xmlStream.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible to open the japanese dictionnary : " + ex.Message);
                throw;
            }
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
