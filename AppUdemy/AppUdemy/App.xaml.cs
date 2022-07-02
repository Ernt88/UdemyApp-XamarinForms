using AppUdemy.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppUdemy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationPage nav = new NavigationPage(new CursosListPage());
            MainPage = nav;
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
