using EncryptStringSample;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using trofimon.ViewModel;
using trofimon.Views;
using Xamarin.Forms;

namespace trofimon.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
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

        private string confirmpassword;
        public string ConfirmPassword
        {
            get => confirmpassword;
            set
            {
                confirmpassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }

        public Command SignUpCommand => new Command(() =>
        {
            if (Password == ConfirmPassword)
            {
                SignUp();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("", "Password must be same as above!", "OK");
            }
        });

        private async void SignUp()
        {
            //validação, email ou password não é preenchida
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                //string PasswordCrypt = StringCipher.Encrypt("batlz", Password); //encripta password
                var user = await FirebaseHelper.AddUser(Email, Password);
                //Adiciona User 
                if (user)
                {
                    await App.Current.MainPage.DisplayAlert("Conta Registada com Sucesso", "", "Ok");
                    await App.Current.MainPage.DisplayAlert("A sessão não sera guardade, terá que fazer o login", "", "Ok");
                    //login automatico
                    await App.Current.MainPage.Navigation.PushModalAsync(new Views.Main.MainTab(Email));
                    await App.Current.MainPage.Navigation.PopToRootAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Conta Registada sem Sucesso", "OK");
                }
            }
        }
    }
}