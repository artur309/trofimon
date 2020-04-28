using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using trofimon.Views;
using trofimon.ViewModel;
using trofimon.Utils;
using EncryptStringSample;

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