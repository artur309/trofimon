using EncryptStringSample;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using trofimon.Views;
using trofimon.Views.Form;
using trofimon.Views.Main;
using Xamarin.Forms;


//private static ISettings AppSettings => CrossSettings.Current;

namespace trofimon.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
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
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                var user = await FirebaseHelper.GetUser(Email);
                //firebase retorna valor nulo se os dados do utilizador não serem encontrados
                if (user != null)
                {
                    string PasswordDecrypt = StringCipher.Decrypt("batlz", user.Password); //desencripta password
                    if (Email == user.Email && PasswordDecrypt == user.Password)
                    {
                        //da-se o login e é enviado a informacao do utilizador pra mainPage
                        await App.Current.MainPage.DisplayAlert("Login Success", "", "Ok");
                        await App.Current.MainPage.Navigation.PushAsync(new MainTab(Email));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                }
            }
        }
    }
}