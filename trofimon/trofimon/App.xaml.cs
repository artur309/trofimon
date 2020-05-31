﻿using System;
using System.Diagnostics;
using trofimon.ViewModel;
using trofimon.Views;
using trofimon.Views.Form;
using trofimon.Views.Main;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace trofimon
{
    public partial class App : Application
    {
        LoginViewModel loginViewModel = new LoginViewModel();
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage();
            //MainPage = new NavigationPage(new Views.Main.MainTab("41"));
            MainPage = new NavigationPage(new Intro());
            //MainPage = new NavigationPage(new FormTab());
        }

        // Handle when your app starts
        protected override void OnStart()
        {
            if (loginViewModel.lembrarSessao == true)
            {
                //verifficar se tem sessao variabel
                if (Current.Properties.ContainsKey("Email"))
                {
                    //App.Current.MainPage.Navigation.PopAsync();
                    string userId = loginViewModel.Email;
                    Current.MainPage.Navigation.PushModalAsync(new MainTab(Preferences.Get(userId, userId)));
                }
                Current.MainPage.DisplayAlert("Ligado", "", "Ok");
            }
            else
            {
                //MainPage = new NavigationPage(new FormTab());
                Current.MainPage.DisplayAlert("Desligado", "", "Ok");
            }
        }

        // Handle when your app sleeps
        protected override void OnSleep()
        {
        }

        // Handle when your app resumes
        protected override void OnResume()
        {
            Current.MainPage.DisplayAlert("PUNK where did u go????", "", "Ok");
        }
    }
}
