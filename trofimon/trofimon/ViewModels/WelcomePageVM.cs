using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using trofimon.ViewModels;

namespace trofimon.ViewModel
{
    public class WelcomePageVM : INotifyPropertyChanged
    {
        public WelcomePageVM(string email2)
        {
            Email = email2;
        }
        private string email;

        public string Email
        {
            get => email;
            set => email = value;
        }
        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command UpdateCommand => new Command(Update);

        public Command DeleteCommand => new Command(Delete);

        //For Logout
        public Command LogoutCommand => new Command(() => { App.Current.MainPage.Navigation.PopAsync(); });

        //Update user data
        private async void Update()
        {
            try
            {
                if (!string.IsNullOrEmpty(Password))
                {
                    var isupdate = await FirebaseHelper.UpdateUser(Email, Password);
                    if (isupdate)
                        await App.Current.MainPage.DisplayAlert("Update Success", "", "Ok");
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Record not update", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Password Require", "Please Enter your password", "Ok");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
            }
        }
        //Delete user data
        private async void Delete()
        {
            try
            {
                var isdelete = await FirebaseHelper.DeleteUser(Email);
                if (isdelete)
                    await App.Current.MainPage.Navigation.PopAsync();
                else
                    await App.Current.MainPage.DisplayAlert("Error", "Record not delete", "Ok");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
            }
        }
    }
}