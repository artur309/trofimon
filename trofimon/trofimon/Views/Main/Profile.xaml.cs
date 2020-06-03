using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.ComponentModel;
using trofimon.Models;
using trofimon.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;

namespace trofimon.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public static readonly FirebaseClient firebase = new FirebaseClient("https://trofimon-pap.firebaseio.com/");
        public ObservableCollection<string> ReceitaStringList { get; set; } = new ObservableCollection<string>();
        LoginViewModel loginViewModel = new LoginViewModel();

        public Profile()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var receitas = await firebase
                     .Child("Receitas")
                     .Child(Preferences.Get(loginViewModel.Email, loginViewModel.Email))
                     .OrderByKey()
                     .OnceAsync<Receitas>();

            foreach (var receita in receitas)
                ReceitaStringList.Add(receita.Object.NomeReceita);

            listaViewReceitas.ItemsSource = null;
            listaViewReceitas.ItemsSource = ReceitaStringList;
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            ReceitaStringList.Clear();
            listaViewReceitas.ItemsSource = ReceitaStringList;
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

        private async void listaViewReceitas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ReceitaDetailedView(e.Item.ToString(), Preferences.Get(loginViewModel.Email, loginViewModel.Email)));
        }
    }
}
