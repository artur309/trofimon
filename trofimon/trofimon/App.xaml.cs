using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using trofimon.Views;

namespace trofimon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new Views.Main.MainTab());
            MainPage = new NavigationPage(new Views.Form.Intro());
            //MainPage = new NavigationPage(new FormTab());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
