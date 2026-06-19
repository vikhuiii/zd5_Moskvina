using System;
using Xamarin.Forms;

namespace zd4_moskvina_
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Оборачиваем WelcomePage в NavigationPage
            MainPage = new NavigationPage(new WelcomePage());
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