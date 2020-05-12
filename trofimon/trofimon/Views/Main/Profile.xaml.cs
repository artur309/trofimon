using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trofimon.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        public void LogoutBtn(object sender, EventArgs e)
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                lembrarSessao = false
            };
            Application.Current.SavePropertiesAsync();
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}