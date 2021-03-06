﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using trofimon.Views.Main;
using Xamarin.Essentials;
using Xamarin.Forms;

//private static ISettings AppSettings => CrossSettings.Current;

namespace trofimon.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool lembrarSessao
        {
            get => Preferences.Get("lembrarSessao", false);
            set
            {
                try
                {   //para fazer handle de sessao
                    Preferences.Set("lembrarSessao", value);
                    PropertyChanged(this, new PropertyChangedEventArgs("lembrarSessao"));

                    Application.Current.Properties["lembrarSessao"] = false;
                    Application.Current.SavePropertiesAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error:{e}");
                }
            }
        }

        private string email;
        public string Email
        {
            get => Preferences.Get("Email", email);
            set
            {
                Preferences.Set("Email", value);
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));

                Application.Current.Properties["Email"] = false;
                Application.Current.SavePropertiesAsync();
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public Command LoginCommand => new Command(Login);

        private async void Login()
        {
            //validação, email ou password não é preenchida
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                await App.Current.MainPage.DisplayAlert("Valores vazios", "Insira Email ou Password", "OK");
            else
            {
                var user = await FirebaseHelper.GetUser(Email);
                //firebase retorna valor nulo se os dados do utilizador não serem encontrados
                if (user != null)
                {
                    //string PasswordDecrypt = StringCipher.Decrypt("batlz", user.Password); //desencripta password
                    if (Email == user.Email && Password == user.Password)
                    {
                        //da-se o login e é enviado a informacao do utilizador pra mainPage
                        await App.Current.MainPage.DisplayAlert("Login Sucesso", "", "Ok");
                        await App.Current.MainPage.Navigation.PopAsync();
                        await App.Current.MainPage.Navigation.PopAsync();
                        await App.Current.MainPage.Navigation.PushAsync(new MainTab(Email));
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Falha Login", "Insira os dados corretos", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Falha Login", "Utilizador não encontrado", "OK");
            }
        }
    }
}